using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Material blueDefaultMaterial, orangeDefaultMaterial, blueMaterial, orangeMaterial;
    public GameObject portalPrefab;

    private GameObject bluePortal, orangePortal;
    private PortalParentHandler bluePortalHandler, orangePortalHandler;
    private RenderTexture blueRenderTexture, orangeRenderTexture;
    private Transform cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
        blueRenderTexture = new RenderTexture(512, 1024, 24);
        blueMaterial.SetTexture("Texture2D_e963585dc5b44a5b86d21edb99ea03f2", blueRenderTexture);
        orangeRenderTexture = new RenderTexture(512, 1024, 24);
        orangeMaterial.SetTexture("Texture2D_e963585dc5b44a5b86d21edb99ea03f2", orangeRenderTexture);
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
            bluePortalHandler = bluePortal.GetComponent<PortalParentHandler>();
            bluePortalHandler.AssignMaterial(blueDefaultMaterial);
            bluePortalHandler.DisableCamera();
            UpdatePortals();

            return;
        }

        // place orange portal
        if (Input.GetKeyDown(KeyCode.Mouse1) && CreatePortal(out GameObject newOrangePortal))
        {
            if (orangePortal != null)
            {
                Destroy(orangePortal);
            }
            orangePortal = newOrangePortal;
            orangePortalHandler = orangePortal.GetComponent<PortalParentHandler>();
            orangePortalHandler.AssignMaterial(orangeDefaultMaterial);
            orangePortalHandler.DisableCamera();
            UpdatePortals();
        }
    }

    private bool CreatePortal(out GameObject newPortal)
    {
        newPortal = null;
        // raycast
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo);
        int targetLayer = hitInfo.transform.gameObject.layer;
        if (targetLayer != 9 && targetLayer != 10)
        {
            return false;
        }

        Vector3 newPortalPosition = hitInfo.point + hitInfo.transform.up * 0.01f;
        newPortal = Instantiate(portalPrefab, newPortalPosition, Quaternion.identity);
        // adjust rotation
        newPortal.transform.forward = hitInfo.transform.up * -1;
        // adjust portal rotation to player rotation
        if (targetLayer == 9)
        {
            Vector3 eulerAngles = newPortal.transform.rotation.eulerAngles;
            newPortal.transform.rotation = Quaternion.Euler(eulerAngles.x, transform.rotation.eulerAngles.y, eulerAngles.z);
        }

        return true;
    }

    private void UpdatePortals()
    {
        if (orangePortal != null && bluePortal != null)
        {
            bluePortalHandler.AssignOtherPortal(orangePortalHandler.GetPortalTravel());
            bluePortalHandler.AssignTargetRenderTexture(orangeRenderTexture);
            bluePortalHandler.EnableCamera();
            bluePortalHandler.AssignMaterial(blueMaterial);

            orangePortalHandler.AssignOtherPortal(bluePortalHandler.GetPortalTravel());
            orangePortalHandler.AssignTargetRenderTexture(blueRenderTexture);
            orangePortalHandler.EnableCamera();
            orangePortalHandler.AssignMaterial(orangeMaterial);
        }
    }
}
