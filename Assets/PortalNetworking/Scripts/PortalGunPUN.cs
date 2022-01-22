using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Co-op portal gun
/// </summary>
public class PortalGunPUN : MonoBehaviourPun
{
    private Transform cam;

    private void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        cam = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        // place blue portal
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreatePortal(true);
            return;
        }

        // place orange portal
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreatePortal(false);
        }
    }

    // each client instantiates the portal for themselves
    private void CreatePortal(bool isBlue)
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo);
        int targetLayer = hitInfo.transform.gameObject.layer;
        // portals should only be placed on objects in layer 9 or 10
        if (targetLayer != 9 && targetLayer != 10)
        {
            return;
        }

        // if the target has a marker class that indicates that the portal should be a child gameobject
        // the viewid is required to find that object on all clients
        // -1 means to place the portal not as a child
        int viewID = -1;
        if (hitInfo.transform.gameObject.GetComponent<PortalAsChildMarker>() != null)
        {
            viewID = hitInfo.transform.gameObject.GetComponent<PhotonView>().ViewID;
        }
        
        Vector3 newPortalPosition = hitInfo.point + hitInfo.transform.up * 0.03f; //0.01
        // adjust rotation
        Vector3 forwrd = hitInfo.transform.up * -1;
        // adjust portal rotation to player rotation
        bool hasRelativeRotation = false;
        if (targetLayer == 9)
        {
            hasRelativeRotation = true;
        }
        // raise event
        object[] data = new object[] { newPortalPosition, forwrd, hasRelativeRotation, transform.rotation.eulerAngles.y, isBlue, viewID};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PortalHandlerPUN.CreatePortalEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }
}
