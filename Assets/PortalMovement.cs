using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour,  IPortable
{
    public float speed = 200f;
    public float mouseSensitivity = 200f;
    public Transform cam;

    private float xRotation = 0f;
    //private float yRotation = 0f;
    private Rigidbody rb;

    private float vertical, horizontal, mouseX, mouseY;

    public bool IsPorting { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        IsPorting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        
        //transform.position += transform.right * horizontal * speed * Time.deltaTime + transform.forward * vertical * speed * Time.deltaTime;


        // look
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= (mouseY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.up * mouseX);
    }

    private void FixedUpdate()
    {
        if (IsPorting)
        {
            IsPorting = false;
            return;
        }

        Vector3 tmp = Vector3.zero + transform.right * horizontal + transform.forward * vertical;
        tmp = tmp * speed * Time.deltaTime;
        rb.MovePosition(transform.position + tmp);

        

        float yRotation = mouseX * Time.fixedDeltaTime + rb.rotation.eulerAngles.y;
        
        rb.MoveRotation(Quaternion.Euler(Vector3.up * yRotation));
    }
}
