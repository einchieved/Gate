using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BallCreationScript : MonoBehaviour
{
    public GameObject cubePrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        }
    }
    
    
    
}
