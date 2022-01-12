using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform;
    }

    private void OnCollisionExit(Collision other)
    {
        other.transform.parent = null;
    }
}
