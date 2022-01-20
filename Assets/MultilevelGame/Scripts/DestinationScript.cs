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
            PhotonNetwork.Destroy(other.gameObject);
            photonView.RPC(nameof(LoadLevel), RpcTarget.MasterClient);
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
    public void LoadLevel()
    {
        photonView.RPC(nameof(LoadPrep), RpcTarget.All);
        PhotonNetwork.LoadLevel("Level0" + nextLevel);
    }
}
