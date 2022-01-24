using UnityEngine;

/// <summary>
/// Singleplayer ONLY
/// This class is used to update the assigned portal and needs to be attached to the root gameobject of the portal prefab.
/// This is helpful, because it avoids searching for <c>PortalTravel</c> in the child gameobjects.
/// </summary>
public class PortalParentHandler : MonoBehaviour
{
    public GameObject portal;

    private PortalTravel portalTravelScript;
    private Camera mainCamera;
    private MeshRenderer meshRenderer;
    private bool isInitiated = false;

    /// <see cref="PortalTravel.OtherPortal"/>
    public void AssignOtherPortal(PortalTravel otherPortal)
    {
        Init();
        portalTravelScript.OtherPortal = otherPortal;
    }

    /// <summary>
    /// assigns the rendertexture as target for the portal camera
    /// </summary>
    public void AssignTargetRenderTexture(RenderTexture renderTexture)
    {
        Init();
        mainCamera.targetTexture = renderTexture;
    }

    /// <summary>
    /// assigns a material for the portal
    /// </summary>
    public void AssignMaterial(Material newMaterial)
    {
        Init();
        meshRenderer.material = newMaterial;
    }

    /// <see cref="PortalTravel.Player"/>
    public void AssignPlayer(Transform player)
    {
        Init();
        portalTravelScript.Player = player;
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

    // needs to be manually initialized, because Start() is called after we need access to the fields
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
