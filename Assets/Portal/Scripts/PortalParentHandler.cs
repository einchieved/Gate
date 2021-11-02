using UnityEngine;

public class PortalParentHandler : MonoBehaviour
{
    public GameObject portal;

    private PortalTravel portalTravelScript;
    private Camera mainCamera;
    private MeshRenderer meshRenderer;
    private bool isInitiated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignOtherPortal(PortalTravel otherPortal)
    {
        Init();
        portalTravelScript.OtherPortal = otherPortal;
    }

    public void AssignTargetRenderTexture(RenderTexture renderTexture)
    {
        Init();
        mainCamera.targetTexture = renderTexture;
    }

    public void AssignMaterial(Material newMaterial)
    {
        Init();
        meshRenderer.material = newMaterial;
    }

    public void EnableCamera()
    {
        Init();
        mainCamera.enabled = true;
    }

    public void DisableCamera()
    {
        Init();
        mainCamera.enabled = false;
    }

    public PortalTravel GetPortalTravel()
    {
        Init();
        return portalTravelScript;
    }

    private void Init()
    {
        if (isInitiated)
        {
            return;
        }

        portalTravelScript = portal.GetComponent<PortalTravel>();
        mainCamera = portalTravelScript.mainCmaera.GetComponent<Camera>();
        meshRenderer = portal.GetComponent<MeshRenderer>();
        isInitiated = true;
    }
}
