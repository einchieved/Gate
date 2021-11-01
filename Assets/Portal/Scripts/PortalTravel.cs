using UnityEngine;

public class PortalTravel : MonoBehaviour
{
    public Transform spawnPosition;

    public Transform OtherPortal { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (OtherPortal == null)
        {
            return;
        }
        // prepare object
        IPortable portable = other.GetComponent<IPortable>();
        portable.IsPorting = true;
        portable.PortingDirection = OtherPortal.up;
        portable.PortingFromDirection = transform.up;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        // position
        //rb.position = OtherPortal.position + OtherPortal.forward * 2;
        rb.position = OtherPortal.GetComponent<PortalTravel>().spawnPosition.position;
        // rotation
        float playerAngleDiff = Vector3.SignedAngle(transform.forward * -1, other.transform.forward, Vector3.up);
        float x = rb.rotation.eulerAngles.x;
        float y = OtherPortal.rotation.eulerAngles.y;
        float z = rb.rotation.eulerAngles.z;
        rb.rotation = Quaternion.Euler(x, y + playerAngleDiff, z);
    }
}
