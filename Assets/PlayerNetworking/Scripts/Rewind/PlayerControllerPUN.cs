using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControllerPUN : Controller
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float mouseSensitivity = 1000f;
    public Transform cam;
    
    private float xRotation = 0f;
    
    public Vector3 jump;
    public float jumpForce = 8.0f;

    private ObjectDetectionPUN objectDetection;


    void Start()
    {
        InitStateHandling();
        GetRigidBody();
        objectDetection = new ObjectDetectionPUN(this);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void Update()
    {
        HandleMovement();
        //HandleRewind(KeyCode.Q, KeyCode.W);
    }

    void FixedUpdate()
    {
        objectDetection.DetectObject();
    }

    private void HandleMovement()
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
