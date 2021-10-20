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
        if (Input.GetKeyDown(KeyCode.Mouse0) && CreatePortal(out GameObject newBluePortal))
        {
            if (bluePortal != null)
            {
                Destroy(bluePortal);
            }
            bluePortal = newBluePortal;
            bluePortal.GetComponent<MeshRenderer>().material = blueMaterial;
            UpdatePortals();

            return;
        }

        // place orange portal
        if (Input.GetKeyDown(KeyCode.Mouse1) && CreatePortal(out GameObject neworangePortal))
        {
            if (orangePortal != null)
            {
                Destroy(orangePortal);
            }
            orangePortal = neworangePortal;
            orangePortal.GetComponent<MeshRenderer>().material = orangeMaterial;
            UpdatePortals();
        }
    }

    private bool CreatePortal(out GameObject newPortal)
    {
        newPortal = null;
        // raycast
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo);
        int targetLayer = hitInfo.transform.gameObject.layer;
        if (targetLayer != 6 && targetLayer != 7)
        {
            return false;
        }

        newPortal = Instantiate(portalPrefab, hitInfo.point, hitInfo.transform.rotation);
        // adjust portal rotation to player rotation
        if (targetLayer == 6)
        {
            Vector3 eulerAngles = newPortal.transform.rotation.eulerAngles;
            newPortal.transform.rotation = Quaternion.Euler(eulerAngles.x, transform.rotation.eulerAngles.y + 180, eulerAngles.z);
        }
        return true;
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
