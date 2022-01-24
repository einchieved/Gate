using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Controller handles time manipulation on specific inputs
/// </summary>
public class ObjectController : Controller, ITimeControlable
{
    private void Start()
    {
        GetRigidBody();
        InitStateHandling();
    }

    private void Update()
    {
        
        // if the object is focused rewind is with the specified keys possible
        if (IsFocused)
        {
            HandleRewind(KeyCode.E, KeyCode.T);
        }

        // removes the focus from one object to allow the focus of other objects
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }
    }


    public bool IsFocused { get; set; }
}