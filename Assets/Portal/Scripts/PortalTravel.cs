using UnityEngine;
using static IPortable;

/// <summary>
/// Singleplayer ONLY
/// Script for the behavior of the portal.
/// </summary>
public class PortalTravel : MonoBehaviour
{
    public Transform spawnPosition, mainCmaera, overlayCamera;

    // a reference to the second portal (if one exists)
    public PortalTravel OtherPortal { get; set; }
    // the reference point to calculate the camera position/rotation for this portal
    public Transform Player { get; set; }

    private Quaternion overlayCamStartRotation;
    private Vector3 overlayCamStartPosition;

    private void Start()
    {
        overlayCamStartRotation = overlayCamera.rotation;
        overlayCamStartPosition = overlayCamera.position;
    }

    // content in this method will update the camera in order to show the correct perspective
    // this is only necessary if there are two portals
    private void Update()
    {
        if (OtherPortal == null)
        {
            return;
        }

        // debug lines to check if the view through portals is correct
        Debug.DrawLine(transform.position, Player.position, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red, Time.deltaTime);

        // reset the camera position/rotation so that we can use the absolute position/rotation and don't need to calculate a delta from the previous frame
        overlayCamera.rotation = overlayCamStartRotation;
        overlayCamera.position = overlayCamStartPosition;
        // updates the camera position/rotation
        overlayCamera.RotateAround(transform.position, transform.forward, OtherPortal.GetPlayerAngleDiffY());
        overlayCamera.RotateAround(transform.position, transform.right, OtherPortal.GetPlayerAngleDiffX() * -1);

        // stabalize the camera if the portal is placed on a wall and not on the ceiling or floor
        if (overlayCamera.parent.up == Vector3.up)
        {
            overlayCamera.right = new Vector3(overlayCamera.right.x, 0, overlayCamera.right.z);
        }
    }

    // start the porting process (if there is another portal)
    private void OnTriggerEnter(Collider other)
    {
        if (OtherPortal == null)
        {
            return;
        }
        // prepare object
        IPortable portable = other.GetComponent<IPortable>();
        if (portable == null)
        {
            return;
        }

        portable.CurrentPortingState = PortingState.Started;
        portable.PortingPortal = transform;
        portable.PortingMvmnt.PortingDirection = OtherPortal.transform.up;
        portable.PortingMvmnt.PortingFromDirection = transform.up;
    }

    // calculates the players angle to this portal on the y-axis
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

    // calculates the players angle to this portal on the x-axis
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
