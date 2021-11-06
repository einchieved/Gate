using System;
using DefaultNamespace;
using UnityEngine;

public class TimeReverserPUN
{
    private Rigidbody _rb;
    private StateCollection _statesOverTime;
    private DateTime timeOfMeasurement;
    public State CurrentState { get; set; }


    public TimeReverserPUN(Rigidbody rb, StateCollection statesOverTime)
    {
        _rb = rb;
        _statesOverTime = statesOverTime;
    }

    public void Rewind()
    {
        double diffInSeconds = (DateTime.Now - timeOfMeasurement).TotalSeconds;
        
        if (diffInSeconds > 0.2)
        {
            if (_statesOverTime.Peak())
            { 
                CurrentState = _statesOverTime.Pop();
                _rb.position = CurrentState.Position;
                _rb.velocity = CurrentState.Velocity;
                _rb.rotation = CurrentState.Rotation;
            }
            
            timeOfMeasurement = DateTime.Now;
        }
    }
}
