using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,  IPortable
{
    public float speed = 200f;
    public float mouseSensitivity = 200f;
    public float maxFallSpeed = 50f;
    public Transform cam;

    private float xRotation = 0f;
    private Rigidbody rb;
    private float vertical, horizontal, mouseX, mouseY;
    private Vector3 lastVelocity;

    public bool IsPorting { get; set; }
    public Vector3 PortingDirection { get; set; }
    public Vector3 PortingFromDirection { get; set; }

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
        // look
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation -= (mouseY * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (IsPorting)
        {
            IsPorting = false;
            rb.velocity = DeterminePortingVelocity();
            return;
        }

        lastVelocity = rb.velocity;
        // move
        Vector3 motionDelta = Vector3.zero + transform.right * horizontal + transform.forward * vertical;
        motionDelta = motionDelta * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + motionDelta);
        // look
        float yRotation = mouseX * Time.fixedDeltaTime + rb.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(Vector3.up * yRotation));
    }

    private Vector3 DeterminePortingVelocity()
    {
        // find origin dir
        float x = Mathf.Abs(PortingFromDirection.x);
        float y = Mathf.Abs(PortingFromDirection.y);
        float z = Mathf.Abs(PortingFromDirection.z);
        float updater;
        if (x > y && x > z)
        {
            updater = Mathf.Clamp(lastVelocity.x, -maxFallSpeed, maxFallSpeed); ;
        }
        else if (y > z)
        {
            updater = Mathf.Clamp(lastVelocity.y, -maxFallSpeed, maxFallSpeed);
        }
        else
        {
            updater = Mathf.Clamp(lastVelocity.z, -maxFallSpeed, maxFallSpeed);
        }

        // find exit dir;
        x = Mathf.Abs(PortingDirection.x);
        y = Mathf.Abs(PortingDirection.y);
        z = Mathf.Abs(PortingDirection.z);
        Vector3 change = Vector3.zero;
        if (x > y && x > z)
        {
            change.x = updater;
        }
        else if (y > z)
        {
            change.y = updater;
        }
        else
        {
            change.z = updater;
        }

        return change;
    }
}
