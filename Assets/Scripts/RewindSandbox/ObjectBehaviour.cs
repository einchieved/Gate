using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{

    private Rigidbody _rb;
    private readonly Stack<State> _statesOverTime = new Stack<State>();
    private bool _rewind;
    private const float speed = 1000;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * speed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _rewind = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
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
        }    }
    
}