using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    public float speed = 20f;
    public float mouseSensitivity = 100f;
    public Transform cam;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.right * horizontal * speed * Time.deltaTime+ transform.forward * vertical * speed * Time.deltaTime;


        // look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
