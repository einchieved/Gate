using UnityEngine;

/// <summary>
/// Attaches player to gameobject(platform) by making him to a child object
/// </summary>
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