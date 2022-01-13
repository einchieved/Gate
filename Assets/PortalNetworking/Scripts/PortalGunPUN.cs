using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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

    private void CreatePortal(bool isBlue)
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo);
        int targetLayer = hitInfo.transform.gameObject.layer;
        if (targetLayer != 9 && targetLayer != 10)
        {
            return;
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
        object[] data = new object[] { newPortalPosition, forwrd, hasRelativeRotation, isBlue };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(PortalHandlerPUN.CreatePortalEventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }
}
