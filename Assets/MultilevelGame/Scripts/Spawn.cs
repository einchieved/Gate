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
        GameObject p1 = GameObject.FindWithTag("P1");
        p1.gameObject.transform.position = p1spawn.transform.position;
        GameObject p2 = GameObject.FindWithTag("P2");
        p2.gameObject.transform.position = p2spawn.transform.position;
    }
    
}
