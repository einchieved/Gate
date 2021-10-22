using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectController : Controller
{

    private void Start()
    {
        GetRigidBody();
        _rb.AddForce(transform.forward * 1000);
        InitStateHandling();
    }

    private void Update()
    {
       HandleRewind(KeyCode.E);
    }
    
}