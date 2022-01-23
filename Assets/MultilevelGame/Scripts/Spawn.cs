using Photon.Pun;
using UnityEngine;

/// <summary>
/// First time spawning in a new level
/// </summary>
public class Spawn : MonoBehaviour
{
    public GameObject p1spawn; 
    public GameObject p2spawn; 

    void Start()
    {
        switch (PhotonNetwork.CurrentRoom.PlayerCount)
        {
            case 1: 
                // spawn one player
                GameObject p1 = GameObject.FindWithTag("P1");
                p1.gameObject.transform.position = p1spawn.transform.position;
                break;
            case 2:
                // spawn both player
                GameObject playerOne = GameObject.FindWithTag("P1");
                if (playerOne != null)
                {
                    playerOne.gameObject.transform.position = p2spawn.transform.position;
                }
                
                GameObject playerTwo = GameObject.FindWithTag("P2");
                if (playerTwo != null)
                {
                    playerTwo.gameObject.transform.position = p1spawn.transform.position;
                }
                break;
        }
        
        
    }
    
}
