using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GravityGun : MonoBehaviourPun
{
    
    public Transform playerCamTransform;

    private GameObject objOnGun;

    public float throwForce;
    
    public float maxDistance;
    public float grabDistance;
    public float attractionForce;
    

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Return) && objOnGun == null)
        {
            Ray ray = new Ray(playerCamTransform.position, playerCamTransform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxDistance));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, 1 << 7 | 1 << 8))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < grabDistance)
                {
                    objOnGun = hit.transform.gameObject;
                    objOnGun.transform.position = transform.position;
                    objOnGun.GetComponent<Rigidbody>().isKinematic = true;
                    objOnGun.transform.parent = transform;
                }
                else
                {
                    Vector3 dir = Vector3.Normalize(transform.position - hit.transform.position);
                    hit.transform.GetComponent<Rigidbody>().AddForce(dir * attractionForce * Time.deltaTime, ForceMode.Impulse);
                }
                
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (objOnGun != null)
            {
                objOnGun.GetComponent<Rigidbody>().isKinematic = false;
                objOnGun.transform.parent = null;
                objOnGun.GetComponent<Rigidbody>().AddForce(playerCamTransform.forward * throwForce, ForceMode.Impulse);
                objOnGun = null;
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0.25f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        
    }
    
      
}
