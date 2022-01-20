using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject p1spawn; 
    public GameObject p2spawn; 

    // Start is called before the first frame update
    void Start()
    {
        switch (PhotonNetwork.CurrentRoom.PlayerCount)
        {
            case 1: 
                GameObject p1 = GameObject.FindWithTag("P1");
                p1.gameObject.transform.position = p1spawn.transform.position;
                break;
            case 2:
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
