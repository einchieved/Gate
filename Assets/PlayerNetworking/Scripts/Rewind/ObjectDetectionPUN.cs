using UnityEngine;


public class ObjectDetectionPUN
{
    private PlayerControllerPUN _playerController;
    
    public ObjectDetectionPUN(PlayerControllerPUN playerController)
    {
        _playerController = playerController;
    }

    public void DetectObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectObject();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SelectGroupOfObjects();
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            DetectObjectsWithinSphere(1 << 7 | 1 << 8);
        }
    }

    private void SelectObject()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(_playerController.cam.position, _playerController.cam.TransformDirection(Vector3.forward), out rayHit,
            Mathf.Infinity))
        {
            GameObject gameObject = rayHit.transform.gameObject;
            EnableTimeManipulation(gameObject);
        }
    }

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
    
    private void DetectObjectsWithinSphere(LayerMask layerMask)
    {
        Collider[] sphereHit = Physics.OverlapSphere(_playerController.cam.position, 100, layerMask);

        foreach(Collider q in sphereHit)
        {
            GameObject gameObject = q.transform.gameObject;
            EnableTimeManipulation(gameObject);
        }
    }

    private void EnableTimeManipulation(GameObject gameObject)
    {
        ITimeControlablePUN controlableObject = gameObject.GetComponent<ITimeControlablePUN>();

        if (controlableObject != null)
        {
            controlableObject.IsFocused = true;
        }
    }

}