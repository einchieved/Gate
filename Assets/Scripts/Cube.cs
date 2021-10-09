using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : MonoBehaviour
{
    
    private Rigidbody rigidbody;
    private ArrayList statesOverTime;
    private DateTime storageTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        storageTime = DateTime.Now;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce(transform.forward * 20);

        if ((DateTime.Now - storageTime).TotalSeconds > 2)
        {
            Vector3 position = transform.position;
            Vector3 velocity = rigidbody.velocity;
            
            statesOverTime.Add(new State(position, velocity));

            storageTime = DateTime.Now;
        }
    }
}
