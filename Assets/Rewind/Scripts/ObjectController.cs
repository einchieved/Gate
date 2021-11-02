using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectController : Controller, ITimeControlable
{
    private void Start()
    {
        GetRigidBody();
        InitStateHandling();
    }

    private void Update()
    {
        if (IsFocused)
        {
            HandleRewind(KeyCode.E, KeyCode.T);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }
    }


    public bool IsFocused { get; set; }
}