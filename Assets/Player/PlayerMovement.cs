using UnityEngine;
using static IPortable;

public class PlayerMovement : MonoBehaviour,  IPortable
{
    public float speed = 200f;
    public float mouseSensitivity = 200f;
    public float maxFallSpeed = 50f;
    public Transform cam;

    private float xRotation = 0f;
    private Rigidbody rb;
    private float vertical, horizontal, mouseX, mouseY;
    private PortingMovement portingMovement;

    public PortingState CurrentPortingState { get; set; }
    public PortingMovement PortingMvmnt => portingMovement;
    public Transform PortingPortal { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        portingMovement = GetComponent<PortingMovement>();
        CurrentPortingState = PortingState.NoPorting;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        // look
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation -= (mouseY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (CurrentPortingState != PortingState.NoPorting)
        {
            switch (CurrentPortingState)
            {
                case PortingState.Started:
                    PortalTravel pt = PortingPortal.GetComponent<PortalTravel>();
                    Transform otherPortalTransform = pt.OtherPortal.spawnPosition;
                    portingMovement.InstantiateClone(pt.spawnPosition, otherPortalTransform);
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
            }
            return;
        }

        Move();
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
