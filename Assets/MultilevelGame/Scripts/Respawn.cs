using UnityEngine;

/// <summary>
/// After the player touches a platform which has this script attached, he/she respawns at a specified point
/// </summary>
public class Respawn : MonoBehaviour
{
    public GameObject companionCubeRespawn; 
    public GameObject p1Respawn; 
    public GameObject p2Respawn; 
    
    private void OnCollisionEnter(Collision other)
    {

        // respawn player on a specified point according to his tag
        switch (other.gameObject.tag)
        {
            case "CompanionCube":
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.transform.position = companionCubeRespawn.transform.position;
                break;
            case "P1":
                other.transform.position = p2Respawn.transform.position;
                break;
            case "P2":
                other.transform.position = p1Respawn.transform.position;
                break;
        }
        
    }
}
