using System;
using UnityEngine;

/// <summary>
/// Records the state by retrieving the informations out of the rigidbody of the game object
/// </summary>
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

    /// <summary>
    /// Records the current State of the gameobject by adding it into the state collection
    /// </summary>
    public void RecordStates()
    {
        double diffInSeconds = (DateTime.Now - currentTime).TotalSeconds;
        
        // record a state after the specified period
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