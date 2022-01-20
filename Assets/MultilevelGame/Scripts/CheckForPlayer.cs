using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        player = other.gameObject;
    }
    
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Collision Exit");
        player = null;
    }
}
