using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectController : Controller
{
    private void Start()
    {
        GetRigidBody();
        InitStateHandling();
    }

    private void Update()
    {
       HandleRewind(KeyCode.E, KeyCode.T);
    }

    
}