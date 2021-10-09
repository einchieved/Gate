using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Creation : MonoBehaviour
{
    public GameObject cubePrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(cubePrefab, transform.position, transform.rotation);
        }
    }
    
    
    
}
