using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject companionCubeRespawn; 
    public GameObject p1Respawn; 
    public GameObject p2Respawn; 
    
    private void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "CompanionCube":
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.position = companionCubeRespawn.transform.position;
                break;
            case "P1":
                other.transform.position = p2Respawn.transform.position;
                break;
            case "P2":
                other.transform.position = p1Respawn.transform.position;
                break;
        }
        
    }
}
