using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Material blueMaterial, orangeMaterial;
    public GameObject portalPrefab;

    private GameObject bluePortal, orangePortal;
    private Transform cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // place blue portal
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bluePortal != null)
            {
                Destroy(bluePortal);
            }
            bluePortal = CreatePortal();
            bluePortal.GetComponent<MeshRenderer>().material = blueMaterial;
            UpdatePortals();            
            return;
        }

        // place orange portal
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (orangePortal != null)
            {
                Destroy(orangePortal);
            }
            orangePortal = CreatePortal();
            orangePortal.GetComponent<MeshRenderer>().material = orangeMaterial;
            UpdatePortals();
        }
    }

    private GameObject CreatePortal()
    {/*
        float playerRotation = transform.rotation.eulerAngles.y;
        float portalRotation = (playerRotation + 180) % 360;
        Quaternion portalQuaternion = Quaternion.Euler(0f, portalRotation, 0f);
        */
        // raycast
        Vector3 origin = cam.position;
        Vector3 direction = cam.forward;
        Physics.Raycast(origin, direction, out RaycastHit hitInfo);

        // determine rotation
        Quaternion rotation = hitInfo.transform.rotation;

        // determine position
        Vector3 position = hitInfo.point;

        return Instantiate(portalPrefab, position, rotation);
    }

    private void UpdatePortals()
    {
        if (orangePortal != null && bluePortal != null)
        {
            bluePortal.GetComponent<PortalTravel>().OtherPortal = orangePortal.transform;
            orangePortal.GetComponent<PortalTravel>().OtherPortal = bluePortal.transform;
        }
    }
}
