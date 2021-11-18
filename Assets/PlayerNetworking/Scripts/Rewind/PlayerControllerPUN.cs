using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControllerPUN : ControllerPUN
{
    // Start is called before the first frame update
    public float speed = 20f;
    public float mouseSensitivity = 1000f;
    public Transform cam;
    
    private float xRotation = 0f;
    private float vertical, horizontal, mouseX, mouseY;

    private ObjectDetectionPUN objectDetection;
    Animator anim;



    void Start()
    {
        InitStateHandling();
        GetRigidBody();
        objectDetection = new ObjectDetectionPUN(this);
        anim = GetComponent<Animator>();
        cam.gameObject.SetActive(false);

        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            cam.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        
        HandleMovement();

        
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool ("Run", true);
            anim.SetBool ("Idle", false);
        }
        else
        {
            anim.SetBool ("Run", false);
            anim.SetBool ("Idle", true);
        } 
        
        // move
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        
        // look
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xRotation -= (mouseY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        //HandleRewind(KeyCode.Q, KeyCode.W);
    }

    void FixedUpdate()
    {
        
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        // move
        Vector3 motionDelta = Vector3.zero + transform.right * horizontal + transform.forward * vertical;
        motionDelta = motionDelta * speed * Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + motionDelta);
        
        // look
        float yRotation = mouseX * Time.fixedDeltaTime + _rb.rotation.eulerAngles.y;
        _rb.MoveRotation(Quaternion.Euler(Vector3.up * yRotation));
        
        objectDetection.DetectObject();

    }

    [PunRPC]
    protected override void FreezeAll()
    {
        
    }

    private void HandleMovement()
    { 
       
    }
}
