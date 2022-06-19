using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private const float speed = 5f;

    [SerializeField] private List<Vector3> waypointList;
    [SerializeField] private List<float> waitTimeList;
    private int waypointIndex;

    [SerializeField] private Vector3 aimDirection;

    [SerializeField] private Player player;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;

    private SpriteRenderer spriteRenderer;

    private FieldOfView fieldOfView;

    private Animator animator;
    private bool isRunning;

    private enum State {
        Waiting,
        Moving,
        Alert,
        Busy,
    }

    private State state;
    private float waitTimer;
    private Vector3 lastMoveDir;

    private void Awake()
    {
        animator = GetComponent < Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        state = State.Waiting;
        waitTimer = waitTimeList[0];
        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);        
    }

    void Update()
    {
        switch (state) {
        default:
        case State.Waiting:
            case State.Moving:
            HandleMovement();
            FindTargetPlayer();
            break;
        case State.Alert:
            Alert();
            break;
        case State.Busy:
            break;
        }

        if (fieldOfView != null) {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
        }

        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 10f);        

    }

    private void FindTargetPlayer() {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance) {
            // Player inside viewDistance
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 1.5f) {
                // Player inside Field of View
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null) {
                    // Hit something
                    if (raycastHit2D.collider.gameObject.GetComponent<Player>() != null) {
                        // Hit Player
                        Alert();
                    }
                }
            }
        }
    } 
    private void Alert() {
        state = State.Busy;
        player.enabled = false;

        Vector3 targetPosition = player.GetPosition();
        Vector3 dirToTarget = (targetPosition - GetPosition()).normalized;
        lastMoveDir = dirToTarget;
        if (targetPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        FindObjectOfType<GameManager>().EndGame();
        Material material = Instantiate(fieldOfView.GetComponent<MeshRenderer>().material);
        fieldOfView.GetComponent<MeshRenderer>().material = material;
        material.SetColor("_FaceColor", Color.red);
    }

    private void HandleMovement() {
        switch (state) {
        case State.Waiting:
            waitTimer -= Time.deltaTime;
                if(isRunning) {
                    isRunning = false;
                    animator.SetBool("IsRunning", false);
                }

            if (waitTimer <= 0f) {
                state = State.Moving;
            }
            break;

        case State.Moving:
                if(!isRunning)
                {
                    isRunning = true;
                    animator.SetBool("IsRunning", true);
                }
            Vector3 waypoint = waypointList[waypointIndex];
            if (waypoint.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
                Vector3 waypointDir = (waypoint - transform.position).normalized;
            lastMoveDir = waypointDir;

            float distanceBefore = Vector3.Distance(transform.position, waypoint);
            //animation
            transform.position = transform.position + waypointDir * speed * Time.deltaTime;
            float distanceAfter = Vector3.Distance(transform.position, waypoint);

            float arriveDistance = .1f;
            if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter) {
                // Go to next waypoint
                waitTimer = waitTimeList[waypointIndex];
                waypointIndex = (waypointIndex + 1) % waypointList.Count;
                state = State.Waiting;
            }
            break;
        }
    }    
    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetAimDir() {
        return lastMoveDir;
    }    

    // Function for Level 5 Speed Boost Mechanic
    public void changeViewDistance(float Distance)
    {
        viewDistance = Distance;
        fieldOfView.SetViewDistance(viewDistance);
        //viewDistance = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") { Alert(); }

    }
}
