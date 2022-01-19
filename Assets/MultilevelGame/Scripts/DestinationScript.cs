using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DestinationScript : MonoBehaviour
{
    public int nextLevel;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.CompareTag("CompanionCube"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject playerOne = GameObject.FindWithTag("P1");
                playerOne.transform.parent = null;

                PhotonNetwork.Destroy(other.gameObject);
                PhotonNetwork.LoadLevel("Level0" + nextLevel); 
            }
        }    
    }
}
