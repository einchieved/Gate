using UnityEngine;

/// <summary>
/// Provides different possibilities to detect object in you surroundings
/// </summary>
public class ObjectDetectionPUN
{
    private PlayerControllerPUN _playerController;
    
    public ObjectDetectionPUN(PlayerControllerPUN playerController)
    {
        _playerController = playerController;
    }

    public void DetectObject()
    {
        
        // detect single object
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectObject();
        }

        // detect objects within a layer
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SelectGroupOfObjects();
        }
        
        // detect all objects in a specified surrounding
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            DetectObjectsWithinSphere(1 << 7 | 1 << 8);
        }
    }

    /// <summary>
    /// Detect single object and enables time manipulation 
    /// </summary>
    private void SelectObject()
    {
        RaycastHit rayHit;
        
        // detection
        if (Physics.Raycast(_playerController.cam.position, _playerController.cam.TransformDirection(Vector3.forward), out rayHit,
            Mathf.Infinity))
        {
            GameObject gameObject = rayHit.transform.gameObject;
            EnableTimeManipulation(gameObject);
        }
    }

    /// <summary>
    /// Detects objects within a layer and enables time manipulation for all of them
    /// </summary>
    private void SelectGroupOfObjects()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(_playerController.cam.position, _playerController.cam.TransformDirection(Vector3.forward), out rayHit,
            Mathf.Infinity))
        {
            GameObject gameObject = rayHit.transform.gameObject;
            LayerMask layerMask = 1 << gameObject.layer;
            DetectObjectsWithinSphere(layerMask);
        }
    }
    
    /// <summary>
    /// Detects objects within a specified sorrounding and enables time manipulation for all of them
    /// </summary>
    private void DetectObjectsWithinSphere(LayerMask layerMask)
    {
        Collider[] sphereHit = Physics.OverlapSphere(_playerController.cam.position, 100, layerMask);

        foreach(Collider q in sphereHit)
        {
            GameObject gameObject = q.transform.gameObject;
            EnableTimeManipulation(gameObject);
        }
    }

    /// <summary>
    /// Enables time manipulation by letting this object into the focus
    /// </summary>
    private void EnableTimeManipulation(GameObject gameObject)
    {
        ITimeControlablePUN controlableObject = gameObject.GetComponent<ITimeControlablePUN>();

        if (controlableObject != null)
        {
            controlableObject.IsFocused = true;
        }
    }

}