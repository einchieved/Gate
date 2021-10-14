using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Creation : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(cubePrefab, transform.position, transform.rotation);
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Instantiate(spherePrefab, transform.position, transform.rotation);
        }
    }
    
    
    
}
