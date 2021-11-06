using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CreationPUN : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    
    void Update()
    {
        GameObject gameObject = null;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
            gameObject = Instantiate(cubePrefab, transform.position, transform.rotation);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject = Instantiate(spherePrefab, transform.position, transform.rotation);
        }

        if (gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 1000); 
        }
    }
    
    
    
}
