using Photon.Pun;
using UnityEngine;
using static IPortable;

public class PlayerMovementPUN : MonoBehaviourPun,  IPortable
{
    public float speed = 80f;
    public float mouseSensitivity = 150;
    public float maxFallSpeed = 20f;
    public Transform cam;

    private float xRotation = 0f;
    private Rigidbody rb;
    private float vertical, horizontal, mouseX, mouseY;
    private PortingMovementPUN portingMovement;

    public PortingState CurrentPortingState { get; set; }
    public PortingMovementPUN PortingMvmnt => portingMovement;
    public Transform PortingPortal { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        portingMovement = GetComponent<PortingMovementPUN>();

        if (!photonView.IsMine)
        {
            Destroy(cam.gameObject);
            Destroy(rb);
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(portingMovement);
            return;
        }

        CurrentPortingState = PortingState.NoPorting;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        // move
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        // look
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation -= (mouseY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        switch (CurrentPortingState)
        {
            case PortingState.Started:
                PortalBehaviorPUN pt = PortingPortal.GetComponent<PortalBehaviorPUN>();
                Transform otherPortalTransform = pt.OtherPortal.spawnPosition;
                portingMovement.InstantiateClone(pt.spawnPosition, otherPortalTransform, GetComponent<Renderer>().material);
                gameObject.layer = 12;
                CurrentPortingState = PortingState.InProgress;
                break;
            case PortingState.InProgress:
                Move();
                portingMovement.UpdateClone();
                break;
            case PortingState.Porting:
                portingMovement.SwitchPlaceWithClone();
                portingMovement.UpdateClone();
                CurrentPortingState = PortingState.InProgress;
                break;
            case PortingState.Ending:
                portingMovement.DestroyClone();
                gameObject.layer = 3; //Player
                CurrentPortingState = PortingState.NoPorting;
                break;
            default:
                Move();
                break;
        }
    }

    private void Move()
    {
        // move
        Vector3 motionDelta = Vector3.zero + transform.right * horizontal + transform.forward * vertical;
        motionDelta = motionDelta * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + motionDelta);
        // look
        float yRotation = mouseX * Time.fixedDeltaTime + rb.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(Vector3.up * yRotation));

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        Vector3 vel = rb.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxFallSpeed, maxFallSpeed);
        vel.y = Mathf.Clamp(vel.y, -maxFallSpeed, maxFallSpeed);
        vel.z = Mathf.Clamp(vel.z, -maxFallSpeed, maxFallSpeed);
        rb.velocity = vel;
    }
}
