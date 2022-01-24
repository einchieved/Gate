using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Class for placing and updating portals.
/// Both players need this script.
/// </summary>
public class PortalHandlerPUN : MonoBehaviourPun, IOnEventCallback
{
    public const byte CreatePortalEventCode = 1;

    // different materials for the portals
    public Material blueDefaultMaterial, orangeDefaultMaterial, blueMaterial, orangeMaterial;
    public GameObject portalPrefab;
    public Transform portalCamRefPoint;

    private GameObject bluePortal, orangePortal;
    // the portals are updated via the PortalParentHandler
    private PortalParentHandlerPUN bluePortalHandler, orangePortalHandler;
    // target rendertextures for portal cameras
    private RenderTexture blueRenderTexture, orangeRenderTexture;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        // Create new rendertextures and assign them to the materials. These materials were created using ShaderGraph.
        blueRenderTexture = new RenderTexture(512, 1024, 24);
        blueMaterial.SetTexture("Texture2D_e963585dc5b44a5b86d21edb99ea03f2", blueRenderTexture);
        orangeRenderTexture = new RenderTexture(512, 1024, 24);
        orangeMaterial.SetTexture("Texture2D_e963585dc5b44a5b86d21edb99ea03f2", orangeRenderTexture);
    }

    public void CallRPC(string methodName, RpcTarget target, params object[] parameters)
    {
        photonView.RPC(methodName, target, parameters);
    }

    public void CreatePortal(Vector3 pos, Vector3 forwrd, bool hasRelativeRotation, float relativeYRotation, bool isBlue, Transform platform)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        GameObject newPortal;
        if (isBlue)
        {
            // create blue portal

            if (bluePortal != null)
            {
                Destroy(bluePortal);
            }
            bluePortal = newPortal = Instantiate(portalPrefab, pos, Quaternion.identity);
            bluePortalHandler = bluePortal.GetComponent<PortalParentHandlerPUN>();
            bluePortalHandler.AssignMaterial(blueDefaultMaterial);
            bluePortalHandler.DisableCamera();
            bluePortalHandler.AssignPlayer(portalCamRefPoint);
        }
        else
        {
            // create orange portal

            if (orangePortal != null)
            {
                Destroy(orangePortal);
            }
            orangePortal = newPortal = Instantiate(portalPrefab, pos, Quaternion.identity);
            orangePortalHandler = orangePortal.GetComponent<PortalParentHandlerPUN>();
            orangePortalHandler.AssignMaterial(orangeDefaultMaterial);
            orangePortalHandler.DisableCamera();
            orangePortalHandler.AssignPlayer(portalCamRefPoint);
        }


        newPortal.transform.forward = forwrd;

        // set portal as child object
        if (platform != null)
        {
            newPortal.transform.parent = platform;
            newPortal.transform.localPosition = Vector3.zero + AbsVector3(platform.up) * 0.03f;
        }

        if (hasRelativeRotation)
        {
            Vector3 eulerAngles = newPortal.transform.rotation.eulerAngles;
            newPortal.transform.rotation = Quaternion.Euler(eulerAngles.x, relativeYRotation, eulerAngles.z);
        }
        UpdatePortalMaterials();
    }

    private void UpdatePortalMaterials()
    {
        if (bluePortal != null && orangePortal != null)
        {
            bluePortalHandler.AssignOtherPortal(orangePortalHandler.GetPortalBehavior());
            bluePortalHandler.AssignTargetRenderTexture(orangeRenderTexture);
            bluePortalHandler.AssignMaterial(blueMaterial);

            orangePortalHandler.AssignOtherPortal(bluePortalHandler.GetPortalBehavior());
            orangePortalHandler.AssignTargetRenderTexture(blueRenderTexture);
            orangePortalHandler.AssignMaterial(orangeMaterial);

            bluePortalHandler.EnableCamera();
            orangePortalHandler.EnableCamera();
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CreatePortalEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 pos = (Vector3)data[0];
            Vector3 forwrd = (Vector3)data[1];
            bool hasRelativeRotation = (bool)data[2];
            float yRot = (float)data[3];
            bool isBlue = (bool)data[4];
            int viewID = (int) data[5];

            Transform platform = null;
            if (viewID != -1)
            {
                platform = PhotonView.Find(viewID).gameObject.transform;
            }
            
            CreatePortal(pos, forwrd, hasRelativeRotation, yRot, isBlue, platform);
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // makes all values of a vector absolute
    // helpfull when placing portals as a child object
    private Vector3 AbsVector3(Vector3 vector)
    {
        Vector3 absVector = Vector3.zero;
        absVector.x = Mathf.Abs(vector.x);
        absVector.y = Mathf.Abs(vector.y);
        absVector.z = Mathf.Abs(vector.z);
        return absVector;
    }
}
