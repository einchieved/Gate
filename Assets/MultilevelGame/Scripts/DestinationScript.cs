using Photon.Pun;
using UnityEngine;

public class DestinationScript : MonoBehaviourPun
{
    public int nextLevel;
    private void OnCollisionEnter(Collision other)
    {
        Debug.LogError("Trigger entered");
        if (other.gameObject.CompareTag("CompanionCube"))
        {
            photonView.RPC(nameof(LoadLevel), RpcTarget.MasterClient, other);
        }    
    }

    [PunRPC]
    public void LoadPrep()
    {
        GameObject playerOne = GameObject.FindWithTag("P1");
        playerOne.transform.parent = null;
        DontDestroyOnLoad(playerOne);

        GameObject playerTwo = GameObject.FindWithTag("P2");
        playerTwo.transform.parent = null;
        DontDestroyOnLoad(playerTwo);
    }

    [PunRPC]
    public void LoadLevel(Collision other)
    {
        photonView.RPC(nameof(LoadPrep), RpcTarget.All);
        PhotonNetwork.Destroy(other.gameObject);
        PhotonNetwork.LoadLevel("Level0" + nextLevel);
    }
}
