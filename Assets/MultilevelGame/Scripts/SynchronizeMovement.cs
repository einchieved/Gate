using Photon.Pun;
using UnityEngine;

public class SynchronizeMovement : MonoBehaviourPun
{
    
    public Vector3 correctPosition;

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            photonView.RPC(nameof(enableAnimator), RpcTarget.All);
        }
    }

    [PunRPC]
    public void enableAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }

    /*
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Serialize");
        if (stream.IsWriting)
        {         
            stream.SendNext(transform.position);
        }
        else
        {         
            correctPosition = (Vector3)stream.ReceiveNext();
        }
    }
    */
    
}
