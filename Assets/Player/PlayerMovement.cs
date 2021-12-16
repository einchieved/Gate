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
    public bool IsClone { get; set; }
    public PortingMovement PortingMvmnt => portingMovement;
    public Transform PortingPortal { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        portingMovement = GetComponent<PortingMovement>();
        CurrentPortingState = PortingState.Ended;
        if (IsClone)
        {
            cam.GetComponent<Camera>().enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClone)
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

        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (IsClone)
        {
            return;
        }
        if (CurrentPortingState != PortingState.Ended)
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
                case PortingState.EndingPositive:
                    portingMovement.TransferControlToClone();
                    CurrentPortingState = PortingState.Ended;
                    break;
                case PortingState.EndingNegative:
                    portingMovement.DestroyClone();
                    gameObject.layer = 3; //Player
                    CurrentPortingState = PortingState.Ended;
                    break;
            }
            return;
        }

        Move();
    }

    public void Declonify(GameObject oldGameObject)
    {
        IsClone = false;
        rb.isKinematic = false;
        //Camera Handling
        cam.GetComponent<Camera>().enabled = true;
        cam.GetComponent<AudioListener>().enabled = true;
        oldGameObject.GetComponentInChildren<Camera>().enabled = false;
        oldGameObject.GetComponentInChildren<AudioListener>().enabled = false;

        PortalGun pg = GetComponent<PortalGun>();
        PortalGun oldPg = oldGameObject.GetComponent<PortalGun>();
        pg.BluePortalHandler = oldPg.BluePortalHandler;
        pg.OrangePortalHandler = oldPg.OrangePortalHandler;
        pg.AssignCurrentPlayerToPortals();
        gameObject.layer = 12; //PortingPlayer   

        PortingPortal = oldGameObject.GetComponent<IPortable>().PortingPortal.GetComponent<PortalTravel>().OtherPortal.transform;
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
