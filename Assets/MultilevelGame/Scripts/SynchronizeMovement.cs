using Photon.Pun;
using UnityEngine;

/// <summary>
/// Synchroniation of moving platforms over the network
/// </summary>
public class SynchronizeMovement : MonoBehaviourPun
{
    
    public Vector3 correctPosition;

    private void Start()
    {
        
        // start platform if both player have entered the room 
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            photonView.RPC(nameof(enableAnimator), RpcTarget.All);
        }
    }

    // enable animator at the same time to make sure the platforms are moving snychronized
    [PunRPC]
    public void enableAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }
}
