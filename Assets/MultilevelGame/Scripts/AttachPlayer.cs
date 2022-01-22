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