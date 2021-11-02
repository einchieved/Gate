using UnityEngine;

public class PortalTravel : MonoBehaviour
{
    public Transform spawnPosition, mainCmaera, overlayCamera;

    public PortalTravel OtherPortal { get; set; }
    public Transform Player { get; set; }

    private Quaternion overlayCamStartRotation;
    private Vector3 overlayCamStartPosition;

    private void Start()
    {
        overlayCamStartRotation = overlayCamera.rotation;
        overlayCamStartPosition = overlayCamera.position;
    }

    private void Update()
    {
        if (OtherPortal == null)
        {
            return;
        }

        Vector3 otherCenter = OtherPortal.GetRotationCenter();
        Vector3 delta = otherCenter - Player.position;
        float angleRotateY = Vector3.SignedAngle(transform.up * -1, delta, Vector3.up);
        overlayCamera.rotation = overlayCamStartRotation;
        overlayCamera.position = overlayCamStartPosition;
        overlayCamera.RotateAround(transform.position, Vector3.up, angleRotateY);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OtherPortal == null)
        {
            return;
        }
        // prepare object
        IPortable portable = other.GetComponent<IPortable>();
        portable.IsPorting = true;
        portable.PortingDirection = OtherPortal.transform.up;
        portable.PortingFromDirection = transform.up;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        // position
        //rb.position = OtherPortal.position + OtherPortal.forward * 2;
        rb.position = OtherPortal.GetComponent<PortalTravel>().spawnPosition.position;
        // rotation
        float playerAngleDiff = Vector3.SignedAngle(transform.forward * -1, other.transform.forward, Vector3.up);
        float x = rb.rotation.eulerAngles.x;
        float y = OtherPortal.transform.rotation.eulerAngles.y;
        float z = rb.rotation.eulerAngles.z;
        rb.rotation = Quaternion.Euler(x, y + playerAngleDiff, z);
    }

    public Vector3 GetRotationCenter()
    {
        return transform.position;
    }
}
