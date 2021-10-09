using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Material blueMaterial, orangeMaterial;
    public GameObject portalPrefab;

    private GameObject bluePortal, orangePortal;

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
    {
        float playerRotation = transform.rotation.eulerAngles.y;
        float portalRotation = (playerRotation + 180) % 360;
        Quaternion portalQuaternion = Quaternion.Euler(0f, portalRotation, 0f);
        return Instantiate(portalPrefab, transform.position + transform.forward * 2, portalQuaternion);
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
