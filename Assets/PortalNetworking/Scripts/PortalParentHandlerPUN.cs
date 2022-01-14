using Photon.Pun;
using UnityEngine;

public class PortalParentHandlerPUN : MonoBehaviourPun
{
    public GameObject portal;

    private PortalBehaviorPUN portalBehavior;
    private Camera mainCamera;
    private MeshRenderer meshRenderer;
    private bool isInitiated = false;

    public void AssignOtherPortal(PortalBehaviorPUN otherPortal)
    {
        Init();
        portalBehavior.OtherPortal = otherPortal;
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

    public void AssignPlayer(Transform player)
    {
        Init();
        portalBehavior.Player = player;
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

    public PortalBehaviorPUN GetPortalBehavior()
    {
        Init();
        return portalBehavior;
    }

    // Need to initlize manually, because the other methods are called directly after the prefab, to which this script is attached,
    // has been instantiated. Start() and Awake() would be called before the next frame, but we need those fields now.
    private void Init()
    {
        if (isInitiated)
        {
            return;
        }

        portalBehavior = portal.GetComponent<PortalBehaviorPUN>();
        mainCamera = portalBehavior.mainCmaera.GetComponent<Camera>();
        meshRenderer = portal.GetComponent<MeshRenderer>();
        isInitiated = true;
    }
}
