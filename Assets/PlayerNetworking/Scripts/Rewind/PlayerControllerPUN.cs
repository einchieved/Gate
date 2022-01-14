using Photon.Pun;
using UnityEngine;

public class PlayerControllerPUN : ControllerPUN
{
    public Transform cam;

    private ObjectDetectionPUN objectDetection;

    void Start()
    {
        InitStateHandling();
        GetRigidBody();
        objectDetection = new ObjectDetectionPUN(this);
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        objectDetection.DetectObject();
    }

    [PunRPC]
    protected override void FreezeAll()
    {
        
    }
}
