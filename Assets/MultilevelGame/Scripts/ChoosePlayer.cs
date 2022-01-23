using Photon.Pun;
using UnityEngine;

/// <summary>
/// Checks if both player have choosen there capability and loads the first level afterwards 
/// </summary>
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
        
        // both player must be on the platform
        if (playercheckPortal == null || playercheckTime == null)
        {
            return;
        }

        // after both player entered a specific platform the first level is loaded
        if (playercheckPortal.player != null && playercheckTime.player != null && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(DestroyAndCreate), RpcTarget.All);
            
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Level01");
            }
        }
    }
    /// <summary>
    /// User gets the choosed player instead of the default one
    /// </summary>
    [PunRPC]
    public void DestroyAndCreate()
    {
        if(playercheckTime.player.GetComponent<PhotonView>().IsMine)
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




