using Photon.Pun;
using UnityEngine;

public class ChoosePlayer : MonoBehaviourPun
{

    public GameObject startTime;
    public GameObject startPortal;
    
    public GameObject timePlayer;
    public GameObject portalPlayer;
    
    private CheckForPlayer playercheckPortal;
    private CheckForPlayer playercheckTime;
    

    void Awake()
    {
        playercheckPortal = startPortal.GetComponent<CheckForPlayer>();
        playercheckTime = startTime.GetComponent<CheckForPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercheckPortal == null || playercheckTime == null)
        {
            return;
        }

        if (playercheckPortal.player != null && playercheckTime.player != null && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(DestroyAndCreate), RpcTarget.All);
            
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Level01");
            }
        }
    }

    [PunRPC]
    public void DestroyAndCreate()
    {
        if(playercheckTime.player.GetComponent<PhotonView>().IsMine) //oder isMine
        {
            PhotonNetwork.Destroy(playercheckTime.player);
            PhotonNetwork.Instantiate(timePlayer.name, new Vector3(), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Destroy(playercheckPortal.player);
            PhotonNetwork.Instantiate(portalPlayer.name, new Vector3(), Quaternion.identity);
        }
    }
}




