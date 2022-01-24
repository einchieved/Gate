using Photon.Pun;
using UnityEngine;
using static IPortable;

public class CompanionCubeMovementPUN : MonoBehaviourPun, IPortable, IPunObservable
{
    private PortingMovementPUN portingMovement;
    private Rigidbody rb;
    private Vector3 lastVelocity, lastPosition;
    private PortingState currentPortingState;

    public PortingState CurrentPortingState { get => currentPortingState; set => SetCurrentPortingState(value); }
    public Transform PortingPortal { get; set; }
    public PortingMovementPUN PortingMvmnt => portingMovement;

    // update field via method, because photonView.IsMine cannot be called in property
    private void SetCurrentPortingState(PortingState val)
    {
        if (photonView.IsMine)
        {
            currentPortingState = val;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        portingMovement = GetComponent<PortingMovementPUN>();
        currentPortingState = PortingState.NoPorting;
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
        // the content of this method should only be executed once
        if (!photonView.IsMine)
        {
            return;
        }

        // prtingstate handling
        switch (CurrentPortingState)
        {
            case PortingState.Started:
                PortalBehaviorPUN pt = PortingPortal.GetComponent<PortalBehaviorPUN>();
                Transform otherPortalTransform = pt.OtherPortal.spawnPosition;
                portingMovement.InstantiateClone(pt.spawnPosition, otherPortalTransform);
                gameObject.layer = 12; //Porting Layer
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
        if (CurrentPortingState == PortingState.Started && collision.collider.gameObject.layer == 16)
        {
            Debug.LogError("collision reset");
            rb.position = lastPosition;
            rb.velocity = lastVelocity;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(CurrentPortingState);
            stream.SendNext(gameObject.layer);
            stream.SendNext(rb.isKinematic); // this property is not syncronized by PUN
        }
        else
        {
            CurrentPortingState = (PortingState)stream.ReceiveNext();
            gameObject.layer = (int)stream.ReceiveNext();
            rb.isKinematic = (bool)stream.ReceiveNext();
        }
    }
}
