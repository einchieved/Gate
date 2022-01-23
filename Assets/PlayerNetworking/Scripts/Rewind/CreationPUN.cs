using Photon.Pun;
using UnityEngine;

/// <summary>
/// Creates objects the player can interact with
/// </summary>
public class CreationPUN : MonoBehaviourPun
{
    public GameObject spherePrefab;
    
    void Update()
    {
        // enables the object creation only for the own player
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        
        GameObject gameObject = null;
        
        // instantiate projectile
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            gameObject = PhotonNetwork.Instantiate(spherePrefab.name, transform.position, transform.rotation);
        }
        
        // let the created object move in a direction
        if (gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 2500); 
        }
    }
    
    
    
}
