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

        other.transform.position = OtherPortal.position + OtherPortal.forward;
        float playerAngleDiff = Vector3.SignedAngle(transform.forward * -1, other.transform.forward, Vector3.up);
        other.transform.rotation = OtherPortal.rotation;
        other.transform.Rotate(Vector3.up * playerAngleDiff);
    }
}
