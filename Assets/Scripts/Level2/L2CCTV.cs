using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2CCTV : MonoBehaviour
{
    [SerializeField] private Vector3 aimDirection;

    [SerializeField] private L2Player player;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private float speed;

    public LayerMask layerMask;
    private CameraAnim cameraAnim;
    private FieldOfView fieldOfView;

    private void Awake()
    {
        cameraAnim = GetComponent<CameraAnim>();
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
        state = State.Surveilling;
        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        StartCoroutine(LerpPosition(positionToMoveTo, speed));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
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

            cameraAnim.UpdateSprite(GetAimDir().x);
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

    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 1.5f)
            {
                // Player inside Field of View
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

        Destroy(this);
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
