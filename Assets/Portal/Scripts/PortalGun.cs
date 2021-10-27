using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Material blueDefaultMaterial, orangeDefaultMaterial, blueMaterial, orangeMaterial;
    public GameObject portalPrefab;

    private GameObject bluePortal, orangePortal;
    private MeshRenderer blueMeshRenderer, orangeMeshRenderer;
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
            blueMeshRenderer = bluePortal.GetComponent<MeshRenderer>();
            blueMeshRenderer.material = blueDefaultMaterial;
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
            orangeMeshRenderer = orangePortal.GetComponent<MeshRenderer>();
            orangeMeshRenderer.material = orangeDefaultMaterial;
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

        newPortal = Instantiate(portalPrefab, hitInfo.point, hitInfo.transform.rotation);
        newPortal.transform.forward = hitInfo.transform.up;
        // adjust portal rotation to player rotation
        if (targetLayer == 9)
        {
            Vector3 eulerAngles = newPortal.transform.rotation.eulerAngles;
            newPortal.transform.rotation = Quaternion.Euler(eulerAngles.x, transform.rotation.eulerAngles.y + 180, eulerAngles.z);
        }

        newPortal.GetComponentInChildren<Camera>().enabled = false;
        return true;
    }

    private void UpdatePortals()
    {
        if (orangePortal != null && bluePortal != null)
        {
            Camera tmpCam;
            bluePortal.GetComponent<PortalTravel>().OtherPortal = orangePortal.transform;
            tmpCam = bluePortal.GetComponentInChildren<Camera>();
            tmpCam.targetTexture = orangeRenderTexture;
            tmpCam.enabled = true;
            blueMeshRenderer.material = blueMaterial;

            orangePortal.GetComponent<PortalTravel>().OtherPortal = bluePortal.transform;
            tmpCam = orangePortal.GetComponentInChildren<Camera>();
            tmpCam.targetTexture = blueRenderTexture;
            tmpCam.enabled = true;
            orangeMeshRenderer.material = orangeMaterial;
        }
    }
}
