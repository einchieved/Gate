using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    private StateCollection _statesOverTime;
    private bool _rewind;
    private const float speed = 1000;
    private double diffInSecondsStateRecording;
    private double diffInSecondsRewind;
    private DateTime currentTimeForRewind;
    private DateTime timeOfMeasurementForRewind;
    private DateTime currentTimeForStateRecording;
    private DateTime timeOfMeasurementForStateRecording;
    private State state;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * speed);
        _statesOverTime = new StateCollection();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _rewind = true;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _rewind = false;
            _rb.useGravity = true;
            _rb.isKinematic = false;
            
            _rb.position = state.Position;
            _rb.velocity = state.Velocity;
            _rb.rotation = state.Rotation;
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
        timeOfMeasurementForStateRecording = DateTime.Now;
        diffInSecondsStateRecording = (timeOfMeasurementForStateRecording - currentTimeForStateRecording).TotalSeconds;
        
        if (diffInSecondsStateRecording > 0.1)
        { 
            var position = _rb.position;
            var velocity = _rb.velocity;
            var rotation = _rb.rotation;
            _statesOverTime.Push(new State(position, velocity, rotation));
            
            currentTimeForStateRecording = DateTime.Now;
        }
        
        
    }

    void Rewind()
    {
        timeOfMeasurementForRewind = DateTime.Now;
        diffInSecondsRewind = (timeOfMeasurementForRewind - currentTimeForRewind).TotalSeconds;
        
        Debug.Log(diffInSecondsRewind);

        if (diffInSecondsRewind > 0.2)
        {
            if (_rewind && _statesOverTime.Peak())
            { 
                state = _statesOverTime.Pop();
                _rb.position = state.Position;
                _rb.velocity = state.Velocity;
                _rb.rotation = state.Rotation;
            }

            if (!_statesOverTime.Peak())
            {
                Destroy(gameObject);
            }
            currentTimeForRewind = DateTime.Now;
        }
    }
    
}