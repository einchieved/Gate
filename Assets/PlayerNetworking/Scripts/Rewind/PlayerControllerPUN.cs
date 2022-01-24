using Photon.Pun;
using UnityEngine;

/// <summary>
/// Enables time manipulation for player
/// </summary>
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
        /// enable object detection only for my own player
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
    
    [PunRPC]
    protected override void ResetAll()
    {
    }
}
