using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Guard : MonoBehaviour
{
    private float speed = 5f;

    [SerializeField] private List<Vector3> waypointList;
    [SerializeField] private List<float> waitTimeList;
    private int waypointIndex;

    [SerializeField] private Vector3 aimDirection;

    private L2Player player;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;

    public bool cctv;

    private FieldOfView fieldOfView;

    //public Animator animator;

    private enum State
    {
        Waiting,
        Moving,
        Alert,
        Busy,
    }

    private State state;
    private float waitTimer;
    private Vector3 lastMoveDir;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<L2Player>();
        state = State.Waiting;
        if (!cctv)
        {
            waitTimer = waitTimeList[0];
        }

        lastMoveDir = aimDirection;
        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.transform.parent = transform;
        fieldOfView.transform.localPosition = new Vector3(0, 0, -5);
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Waiting:
            case State.Moving:
                if (!cctv)
                {
                    HandleMovement();
                }
                FindTargetPlayer();
                break;
            case State.Alert:
                Alert();
                break;
            case State.Busy:
                break;
        }

        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
        }

        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 10f);
    }

    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            // Player inside viewDistance
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
            {
                // Player inside Field of View
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null)
                {
                    // Hit something
                    if (raycastHit2D.collider.gameObject.GetComponent<L2Player>() != null)
                    {
                        // Hit Player
                        Alert();
                    }
                    else
                    {
                        // Hit something else
                    }
                }
            }
        }
    }
    private void Alert()
    {
        state = State.Busy;

        Vector3 targetPosition = player.GetPosition();
        Vector3 dirToTarget = (targetPosition - GetPosition()).normalized;
        lastMoveDir = dirToTarget;


        FindObjectOfType<GameManager>().EndGame();
        //animator.Play("Guard");
        //play anim
        //Alert other guards
        //gameover
    }
    private void HandleMovement()
    {
        switch (state)
        {
            case State.Waiting:
                waitTimer -= Time.deltaTime;
                //animation
                if (waitTimer <= 0f)
                {
                    state = State.Moving;
                }
                break;
            case State.Moving:
                Vector3 waypoint = waypointList[waypointIndex];

                Vector3 waypointDir = (waypoint - transform.position).normalized;
                lastMoveDir = waypointDir;

                float distanceBefore = Vector3.Distance(transform.position, waypoint);
                //animation
                transform.position = transform.position + waypointDir * speed * Time.deltaTime;
                float distanceAfter = Vector3.Distance(transform.position, waypoint);

                float arriveDistance = .1f;
                if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter)
                {
                    // Go to next waypoint
                    waitTimer = waitTimeList[waypointIndex];
                    waypointIndex = (waypointIndex + 1) % waypointList.Count;
                    state = State.Waiting;
                }
                break;
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetAimDir()
    {
        return lastMoveDir;
    }
}
