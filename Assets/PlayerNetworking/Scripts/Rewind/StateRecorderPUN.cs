using System;
using UnityEngine;

public class StateRecorderPUN
{
    
    private Rigidbody _rb;
    private StateCollectionPUN _statesOverTime;
    private DateTime currentTime;
 
    public StateRecorderPUN(Rigidbody rb, StateCollectionPUN statesOverTime)
    {
        _rb = rb;
        _statesOverTime = statesOverTime;
    }

    public void RecordStates()
    {
        double diffInSeconds = (DateTime.Now - currentTime).TotalSeconds;
        
        if (diffInSeconds > 0.1)
        { 
            var position = _rb.position;
            var velocity = _rb.velocity;
            var rotation = _rb.rotation;
            _statesOverTime.Push(new StatePUN(position, velocity, rotation));
            
            currentTime = DateTime.Now;
        }
    }
}