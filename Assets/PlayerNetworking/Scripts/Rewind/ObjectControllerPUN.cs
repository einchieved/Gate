using System;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class ObjectControllerPUN : ControllerPUN, ITimeControlablePUN
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
            Debug.Log("isFocused");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }
    }
    
    [PunRPC]
    protected override void FreezeAll() {
        base.FreezeAll();
    }


    public bool IsFocused { get; set; }
}