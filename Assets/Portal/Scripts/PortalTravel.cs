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

        Debug.DrawLine(transform.position, Player.position, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red, Time.deltaTime);

        overlayCamera.rotation = overlayCamStartRotation;
        overlayCamera.position = overlayCamStartPosition;
        overlayCamera.RotateAround(transform.position, transform.forward, OtherPortal.GetPlayerAngleDiffY());
        overlayCamera.RotateAround(transform.position, transform.right, OtherPortal.GetPlayerAngleDiffX() * -1);
        /*Vector3 eulerAngles = overlayCamera.localEulerAngles;
        eulerAngles.x = 0;
        overlayCamera.localEulerAngles = eulerAngles;*/
        //overlayCamera.LookAt(transform.position, transform.forward);
        overlayCamera.right = new Vector3(overlayCamera.right.x, 0, overlayCamera.right.z);
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

    public Vector3 GetForward()
    {
        return transform.up;
    }

    public float GetPlayerAngleDiffY()
    {
        Vector3 delta = Player.position - transform.position;
        Vector3 planeProjection = transform.up + transform.right;
        Vector3 planeDelta = Vector3.zero;
        planeDelta.x = delta.x * Mathf.Abs(planeProjection.x);
        planeDelta.y = delta.y * Mathf.Abs(planeProjection.y);
        planeDelta.z = delta.z * Mathf.Abs(planeProjection.z);
        return Vector3.SignedAngle(transform.up, planeDelta, transform.forward);
    }

    public float GetPlayerAngleDiffX()
    {
        Vector3 delta = Player.position - transform.position;
        Vector3 planeProjection = transform.up + transform.forward;
        Vector3 planeDelta = Vector3.zero;
        planeDelta.x = delta.x * Mathf.Abs(planeProjection.x);
        planeDelta.y = delta.y * Mathf.Abs(planeProjection.y);
        planeDelta.z = delta.z * Mathf.Abs(planeProjection.z);
        return Vector3.SignedAngle(transform.up, planeDelta, transform.right);
    }
}
