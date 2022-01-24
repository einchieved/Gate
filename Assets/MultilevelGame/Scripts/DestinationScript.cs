using Photon.Pun;
using UnityEngine;

/// <summary>
/// Attached to the destination of the companion cube 
/// </summary>
public class DestinationScript : MonoBehaviourPun
{
    public int nextLevel;
    private void OnCollisionEnter(Collision other)
    {
        // if the companion cube enters the destination platform, enter the next level
        if (other.gameObject.CompareTag("CompanionCube"))
        {
            PhotonNetwork.Destroy(other.gameObject);
            photonView.RPC(nameof(LoadLevel), RpcTarget.MasterClient);
        }    
    }

    /// <summary>
    /// Prepare both player to enter the next level
    /// </summary>
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

    /// <summary>
    /// Load the next lövel
    /// </summary>
    [PunRPC]
    public void LoadLevel()
    {
        photonView.RPC(nameof(LoadPrep), RpcTarget.All);
        PhotonNetwork.LoadLevel("Level0" + nextLevel);
    }
}
