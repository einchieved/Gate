using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Creates objects the player can interact with
/// </summary>
public class Creation : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    
    void Update()
    {
        // instantiate projectile
        GameObject gameObject = null;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            gameObject = Instantiate(cubePrefab, transform.position, transform.rotation);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject = Instantiate(spherePrefab, transform.position, transform.rotation);
        }

        // let the created object move in a direction
        if (gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 1000); 
        }
    }
    
    
    
}
