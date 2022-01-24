using UnityEngine;

/// <summary>
/// Singleplayer ONLY
/// This class takes care of the teleporting clone and teleporting velocity.
/// </summary>
public class PortingMovement : MonoBehaviour
{
    // the gameobject used to represent a clone during the porting process
    public GameObject cloneGameObject;

    // the exit direction of the teleporting process
    public Vector3 PortingDirection { get; set; }
    // the entry direction of the teleporting process
    public Vector3 PortingFromDirection { get; set; }

    private Rigidbody rb;
    // used to calculate the exit velocity after teleporting
    private Vector3 lastvelocity;
    private Transform clone, originalPortal, clonePortal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // keep the velocity of the current frame
        lastvelocity = rb.velocity;
    }

    public void DestroyClone()
    {
        if (clone != null)
        {
            Destroy(clone.gameObject);
        }
    }

    // switches the objects position with the clone position
    public void SwitchPlaceWithClone()
    {
        rb.position = clone.position;
        rb.rotation = clone.rotation;

        Transform tmpPortal = originalPortal;
        originalPortal = clonePortal;
        clonePortal = tmpPortal;

        clone.parent = clonePortal;

        rb.velocity = DeterminePortingVelocity();
    }

    public void InstantiateClone(Transform originalPortal, Transform clonePortal)
    {
        if (clone != null)
        {
            return;
        }
        this.originalPortal = originalPortal;
        this.clonePortal = clonePortal;
        clone = Instantiate(cloneGameObject, clonePortal.position, Quaternion.identity).transform;
        clone.parent = clonePortal;
        clone.gameObject.layer = 13; //Clone Layer
        UpdateClone();
    }

    #region clone update

    /// <summary>
    /// Updates the clone position/ rotation to the exit portal, depending on
    /// the players position/rotation to the entry portal
    /// </summary>
    public void UpdateClone()
    {
        UpdateClonePosition();
        UpdateCloneRotation();
    }

    private void UpdateClonePosition()
    {
        // determine relative position of original object to portal
        Vector3 distance = transform.position - originalPortal.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, originalPortal.right) * -1;
        relativePosition.y = Vector3.Dot(distance, originalPortal.up) * -1;
        relativePosition.z = Vector3.Dot(distance, originalPortal.forward);
        // set position of clone relative to other portal
        clone.localPosition = relativePosition;
        //Debug.Log(relativePosition);
    }

    private void UpdateCloneRotation()
    {
        float playerAngleDiff = Vector3.SignedAngle(originalPortal.up * -1, transform.forward, Vector3.up);
        clone.forward = clonePortal.up;
        clone.Rotate(new Vector3(0, playerAngleDiff * -1, 0), Space.Self);
    }

    #endregion

    // calculates the velocity of this object when exiting the second portal
    // only a simplification
    private Vector3 DeterminePortingVelocity()
    {
        // find origin dir
        float x = Mathf.Abs(PortingFromDirection.x);
        float y = Mathf.Abs(PortingFromDirection.y);
        float z = Mathf.Abs(PortingFromDirection.z);
        float updater;
        if (x > y && x > z)
        {
            updater = Mathf.Abs(lastvelocity.x);
        }
        else if (y > z)
        {
            updater = Mathf.Abs(lastvelocity.y);
        }
        else
        {
            updater = Mathf.Abs(lastvelocity.z);
        }

        // find exit dir;
        return PortingDirection * updater;
    }
}
