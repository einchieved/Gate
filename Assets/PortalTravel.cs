using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTravel : MonoBehaviour
{
    public Transform otherPortal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = otherPortal.position + otherPortal.forward;
        //float angleDiff = Vector3.Angle(transform.forward, otherPortal.forward);
        float playerAngleDiff = Vector3.SignedAngle(transform.forward * -1, other.transform.forward, Vector3.up);

        other.transform.rotation = otherPortal.rotation;
        other.transform.Rotate(Vector3.up * playerAngleDiff);
    }
}
