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
                PhotonNetwork.Destroy(other.gameObject);
                PhotonNetwork.LoadLevel("Level0" + nextLevel); 
            }
        }    
    }
}
