using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private SpriteRenderer spriteRenderer;

    private FieldOfView fieldOfView;

    public Transform field;
    private bool isRunning;
    private Animator animator;
    private NavMeshAgent nav;
    public bool IsAIOn;
    public LayerMask layerMask;

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<L2Player>();
        if (GetComponent<NavMeshAgent>()) {
            nav = GetComponent<NavMeshAgent>();
            nav.updateRotation = false;
            nav.updateUpAxis = false;
        }
    }

    void Start()
    {
        state = State.Waiting;
        waitTimer = waitTimeList[0];

        lastMoveDir = aimDirection;
        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        if(transform.parent.name == "Main Room Guards")
        {
            fieldOfView.transform.parent = field.transform;
        }
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
                HandleMovement();
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
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, layerMask);
                if (raycastHit2D.collider)
                {
                    Alert();
                }
            }
        }
    }
    private void Alert()
    {
        state = State.Busy;
        player.enabled = false;

        Vector3 targetPosition = player.GetPosition();
        Vector3 dirToTarget = (targetPosition - GetPosition()).normalized;
        lastMoveDir = dirToTarget;

        FindObjectOfType<GameManager>().EndGame();
        Material material = Instantiate(fieldOfView.GetComponent<MeshRenderer>().material);
        fieldOfView.GetComponent<MeshRenderer>().material = material;
        material.SetColor("_FaceColor", Color.red);
    }

    private void HandleMovement()
    {
        switch (state)
        {
            case State.Waiting:
                if(isRunning)
                {
                    isRunning = false;
                    animator.SetBool("IsRunning", false);
                }

                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    state = State.Moving;
                }
                break;

            case State.Moving:
                if(!isRunning)
                {
                    isRunning = true;
                    animator.SetBool("IsRunning", true);
                }

                if (!IsAIOn)
                {
                    Vector3 waypoint = waypointList[waypointIndex];
                    if(waypoint.x < transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                    Vector3 waypointDir = (waypoint - transform.position).normalized;
                    lastMoveDir = waypointDir;

                
                    float distanceBefore = Vector2.Distance(transform.position, waypoint);
                    //animation
                    transform.position = transform.position + waypointDir * speed * Time.deltaTime;
                    float distanceAfter = Vector2.Distance(transform.position, waypoint);

                    float arriveDistance = .1f;
                    if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter)
                    {
                        waitTimer = waitTimeList[waypointIndex];
                        waypointIndex = (waypointIndex + 1) % waypointList.Count;
                        state = State.Waiting;
                    }
                }

                else
                {
                    nav.SetDestination(player.GetPosition());
                    if (player.GetPosition().x < transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                    Vector3 waypointDir = (player.GetPosition() - transform.position).normalized;
                    lastMoveDir = waypointDir;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Alert();
        }
    }
}
