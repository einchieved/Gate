using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float mouseSensitivity = 1000f;
    public Transform cam;
    
    private Rigidbody _rb;
    private readonly Stack<State> _statesOverTime = new Stack<State>();
    private bool _rewind;

    private float xRotation = 0f;
    
    public Vector3 jump;
    public float jumpForce = 8.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

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
        
        if(Input.GetKeyDown(KeyCode.Space)){
            _rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            _rewind = true;
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            _rewind = false;
        }
    }


    private void FixedUpdate()
    {        
        
        if (!_rewind)
        {
            RecordStates();
        }
        else
        {
            Rewind();
        }
    }
    
    void RecordStates()
    {
        var position = _rb.position;
        var velocity = _rb.velocity;
        var rotation = _rb.rotation;
        
        _statesOverTime.Push(new State(position, velocity, rotation));
    }

    void Rewind()
    {
        if (_rewind && _statesOverTime.Count > 0)
        {
            State state = _statesOverTime.Pop();
            
            _rb.position = state.Position;
            _rb.velocity = state.Velocity;
            _rb.rotation = state.Rotation;
        }

        if (_statesOverTime.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
