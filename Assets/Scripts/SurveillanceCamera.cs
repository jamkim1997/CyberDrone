using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] private Vector3 aimDirection;
    [SerializeField] private Player player;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private float speed;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprite;
    public LayerMask layerMask;
    private FieldOfView fieldOfView;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private enum State
    {
        Surveilling,
        Alert,
        Busy
    }

    private State state;
    private Vector3 lastMoveDir;

    public Vector3 positionToMoveTo;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        state = State.Surveilling;
        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.transform.parent = transform;
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        StartCoroutine(LerpPosition(positionToMoveTo, speed));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state){
            default:
            case State.Surveilling:
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
            UpdateSprite(GetAimDir().x);
        }

        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 10f);
    }

    IEnumerator LerpPosition(Vector3 target, float speed)
    {

        while (true)
        {
        float time = Mathf.PingPong(Time.time * speed, 1);
        //Vector3 startPosition = lastMoveDir;          
        
        
        lastMoveDir = Vector3.Lerp(aimDirection, target, time);
        yield return null;
        }
    }

    private void UpdateSprite(float x)
    {
        if(x < -0.5f)
        {
            spriteRenderer.sprite = sprite[0];
            spriteRenderer.flipX = false;
        }
        else if(x < 0.5f)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = sprite[1];
        }
        else if(x > 0.5f)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = sprite[0];
        }

    }

    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, layerMask);
                if (raycastHit2D.collider != null)
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetAimDir()
    {
        return lastMoveDir;
    }


    bool isApproximate(float a, float b, float tolerance)
    {
        return Mathf.Abs (a - b) < tolerance;
    }


    // Function for Level 5 Switch Off Camera Mechanic
    public void changeViewDistance(float Distance)
    {
        viewDistance = Distance;
        fieldOfView.SetViewDistance(viewDistance);
        //viewDistance = 10;
    }
}
