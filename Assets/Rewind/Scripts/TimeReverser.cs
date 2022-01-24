using System;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Enables the Rewind
/// </summary>
public class TimeReverser
{
    private Rigidbody _rb;
    private StateCollection _statesOverTime;
    private DateTime timeOfMeasurement;
    public State CurrentState { get; set; }


    public TimeReverser(Rigidbody rb, StateCollection statesOverTime)
    {
        _rb = rb;
        _statesOverTime = statesOverTime;
    }

    /// <summary>
    /// Retrive States in a specified time period
    /// </summary>
    public void Rewind()
    {
        double diffInSeconds = (DateTime.Now - timeOfMeasurement).TotalSeconds;
        
        // retrieve state every 0.2 seconds
        if (diffInSeconds > 0.2)
        {
            
            // make sure a state is available and adapt the rigidbody accordingly to the state
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
