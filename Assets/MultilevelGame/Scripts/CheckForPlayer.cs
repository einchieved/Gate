using UnityEngine;

/// <summary>
/// Checks for player of a given plattform 
/// </summary>
public class CheckForPlayer : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter(Collision other)
    {
        player = other.gameObject;
    }
    
    private void OnCollisionExit(Collision other)
    {
        player = null;
    }
}
