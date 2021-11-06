using System;
using DefaultNamespace;
using UnityEngine;

public class StateRecorderPUN
{
    
    private Rigidbody _rb;
    private StateCollection _statesOverTime;
    private DateTime currentTime;
 
    public StateRecorderPUN(Rigidbody rb, StateCollection statesOverTime)
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
            _statesOverTime.Push(new State(position, velocity, rotation));
            
            currentTime = DateTime.Now;
        }
    }
}