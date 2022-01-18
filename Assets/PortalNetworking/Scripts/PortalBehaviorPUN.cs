using Photon.Pun;
using UnityEngine;
using static IPortable;

public class PortalBehaviorPUN : MonoBehaviour
{
    public Transform spawnPosition, mainCmaera, overlayCamera;

    public PortalBehaviorPUN OtherPortal { get; set; }
    public Transform Player { get; set; }

    private Quaternion overlayCamStartRotation;
    private Vector3 overlayCamStartPosition;

    private void Start()
    {
        overlayCamStartRotation = overlayCamera.localRotation;
        overlayCamStartPosition = overlayCamera.localPosition;
        if (Player == null && PlayerManager.LocalPlayerInstance != null)
        {
            Player = PlayerManager.LocalPlayerInstance.transform;
        }
    }

    private void Update()
    {
        if (OtherPortal == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, Player.position, Color.green, Time.deltaTime);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red, Time.deltaTime);
        Debug.DrawRay(overlayCamera.position, overlayCamera.forward * 10, Color.blue, Time.deltaTime);

        overlayCamera.localRotation = overlayCamStartRotation;
        overlayCamera.localPosition = overlayCamStartPosition;
        overlayCamera.RotateAround(transform.position, transform.forward, OtherPortal.GetPlayerAngleDiffY());
        overlayCamera.RotateAround(transform.position, transform.right, OtherPortal.GetPlayerAngleDiffX() * -1);

        if (overlayCamera.parent.up == Vector3.up)
        {
            overlayCamera.right = new Vector3(overlayCamera.right.x, 0, overlayCamera.right.z);
        }
    }

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
        Debug.LogError("change to started");
        portable.PortingPortal = transform;
        portable.PortingMvmnt.PortingDirection = OtherPortal.transform.up;
        portable.PortingMvmnt.PortingFromDirection = transform.up;
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
