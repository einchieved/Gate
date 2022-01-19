using Photon.Pun;
using UnityEngine;

public class PortingMovementPUN : MonoBehaviourPun
{
    public GameObject cloneGameObject;

    public Vector3 PortingDirection { get; set; }
    public Vector3 PortingFromDirection { get; set; }

    private Rigidbody rb;
    private Vector3 lastvelocity;
    private Transform clone, originalPortal, clonePortal;
    private IPortable portable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        portable = GetComponent<IPortable>();
    }

    private void FixedUpdate()
    {
        lastvelocity = rb.velocity;
    }

    public void DestroyClone()
    {
        if (clone != null)
        {
            PhotonNetwork.Destroy(clone.gameObject);
        }
    }

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
        clone = PhotonNetwork.Instantiate(cloneGameObject.name, clonePortal.position, Quaternion.identity).transform;
        clone.parent = clonePortal;
        clone.gameObject.layer = 13; //ClonePlayer
        UpdateClone();
    }

    public void InstantiateClone(Transform originalPortal, Transform clonePortal, Material material)
    {
        InstantiateClone(originalPortal, clonePortal);
        clone.gameObject.GetComponent<Renderer>().material = material;
    }

    #region clone update

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

    private Vector3 DeterminePortingVelocity()
    {
        Debug.LogError("PortVel");
        // find origin dir
        Vector3 portfromDirNorm = PortingFromDirection.normalized;
        float x = Mathf.Abs(portfromDirNorm.x);
        float y = Mathf.Abs(portfromDirNorm.y);
        float z = Mathf.Abs(portfromDirNorm.z);
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
        return PortingDirection.normalized * updater;
    }
}
