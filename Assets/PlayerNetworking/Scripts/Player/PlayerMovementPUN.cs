using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovementPUN : MonoBehaviourPun
{
    public float speed = 200f;
    public float mouseSensitivity = 200f;
    public Transform cam;

    private float xRotation = 0f;
    private Rigidbody rb;
    private float vertical, horizontal, mouseX, mouseY;
    Animator anim;
    

    public bool IsPorting { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        IsPorting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        
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
    }

    private void FixedUpdate()
    {
        // move
        Vector3 motionDelta = Vector3.zero + transform.right * horizontal + transform.forward * vertical;
        motionDelta = motionDelta * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + motionDelta);
            
        // look
        float yRotation = mouseX * Time.fixedDeltaTime + rb.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(Vector3.up * yRotation));
        
        
    }
}
