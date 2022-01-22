using System;
using UnityEngine;

public class TimeReverserPUN
{
    private Rigidbody _rb;
    private StateCollectionPUN _statesOverTime;
    private DateTime timeOfMeasurement;
    public StatePUN CurrentState { get; set; }


    public TimeReverserPUN(Rigidbody rb, StateCollectionPUN statesOverTime)
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
