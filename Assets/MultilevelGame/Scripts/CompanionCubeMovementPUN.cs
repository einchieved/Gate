using UnityEngine;
using static IPortable;

public class CompanionCubeMovementPUN : MonoBehaviour, IPortable
{
    private PortingMovementPUN portingMovement;
    private Rigidbody rb;
    private Vector3 lastVelocity, lastPosition;

    public PortingState CurrentPortingState { get; set; }
    public Transform PortingPortal { get; set; }
    public PortingMovementPUN PortingMvmnt => portingMovement;

    // Start is called before the first frame update
    void Start()
    {
        portingMovement = GetComponent<PortingMovementPUN>();
        CurrentPortingState = PortingState.NoPorting;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
        lastPosition = rb.position;
    }

    private void FixedUpdate()
    {
        switch (CurrentPortingState)
        {
            case PortingState.Started:
                PortalBehaviorPUN pt = PortingPortal.GetComponent<PortalBehaviorPUN>();
                Transform otherPortalTransform = pt.OtherPortal.spawnPosition;
                portingMovement.InstantiateClone(pt.spawnPosition, otherPortalTransform);
                gameObject.layer = 12;
                CurrentPortingState = PortingState.InProgress;
                break;
            case PortingState.InProgress:
                portingMovement.UpdateClone();
                break;
            case PortingState.Porting:
                portingMovement.SwitchPlaceWithClone();
                portingMovement.UpdateClone();
                CurrentPortingState = PortingState.InProgress;
                break;
            case PortingState.Ending:
                portingMovement.DestroyClone();
                gameObject.layer = 8; //CubeTime
                CurrentPortingState = PortingState.NoPorting;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ignore Collision with wall if we now start porting
        // see https://docs.unity3d.com/Manual/ExecutionOrder.html for more information about
        // the order of execution for event functions
        if (CurrentPortingState == PortingState.Started && collision.collider.gameObject.layer == 11)
        {
            rb.position = lastPosition;
            rb.velocity = lastVelocity;
        }
    }
}
