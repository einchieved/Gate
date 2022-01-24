using System;
using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Records the state by retrieving the informations out of the rigidbody of the game object
/// </summary>
public class StateRecorder
{
    
    private Rigidbody _rb;
    private StateCollection _statesOverTime;
    private DateTime currentTime;
 
    public StateRecorder(Rigidbody rb, StateCollection statesOverTime)
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
            _statesOverTime.Push(new State(position, velocity, rotation));
            
            currentTime = DateTime.Now;
        }
    }
}