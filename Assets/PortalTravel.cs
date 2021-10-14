using UnityEngine;

public class PortalTravel : MonoBehaviour
{
    public Transform OtherPortal { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (OtherPortal == null)
        {
            return;
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
        other.GetComponent<IPortable>().IsPorting = true;

        // move
        rb.position = /*new Vector3(0, 2.5f, 0); */OtherPortal.position + OtherPortal.forward * 2;
        //other.transform.position = OtherPortal.position + OtherPortal.forward * 2;
        // rotate
        float playerAngleDiff = Vector3.SignedAngle(transform.forward * -1, other.transform.forward, Vector3.up);
        float x = /*other.transform.rotation*/rb.rotation.eulerAngles.x;
        float y = OtherPortal.rotation.eulerAngles.y;
        float z = /*other.transform.rotation*/rb.rotation.eulerAngles.z;

        rb.rotation = Quaternion.Euler(x, y + playerAngleDiff, z);

        //other.transform.rotation = Quaternion.Euler(x, y, z);
        //other.transform.Rotate(Vector3.up * playerAngleDiff);
    }
}
