using DefaultNamespace;
using UnityEngine;

/// <summary>
/// Deprecated
/// Base class to enable time manipulation on the attached object
/// </summary>
public class Controller: MonoBehaviour
{
    protected Rigidbody _rb;
    protected bool _rewind;
    
    private StateCollection _statesOverTime;
    private TimeReverser timeReverser;
    private StateRecorder stateRecorder;
    private FreezeForceRecorder freezeForceRecorder;


    /// <summary>
    /// Creates all objects necessary to enable time manipulation on the attached object
    /// </summary>
    protected void InitStateHandling()
    {
        _statesOverTime = new StateCollection();
        
        timeReverser = new TimeReverser(_rb, _statesOverTime);
        stateRecorder = new StateRecorder(_rb, _statesOverTime);
        
        freezeForceRecorder = new FreezeForceRecorder(_rb);
    }

    protected void GetRigidBody()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Depending on the input the time manipulation is controlled
    /// </summary>
    /// <param name="rewindKey">Keycode which is used to start the rewind</param>
    /// <param name="endRewindKey">Keycode which is used to end the rewind</param>
    protected void HandleRewind(KeyCode rewindKey, KeyCode endRewindKey)
    {
        // start rewind
        if (Input.GetKeyDown(rewindKey))
        {
            EnableRewind();
        }

        // end rewind
        if (Input.GetKeyDown(endRewindKey))
        {
            DisableRewind();
            AdaptState();
        }
        
        // freeze state
        if (Input.GetKeyDown(KeyCode.R))
        {
            Freeze();
        }
    }
    

    /// <summary>
    /// Retrieves last state and adapts the object accordingly
    /// </summary>
    private void AdaptState()
    {
        State lastState = timeReverser.CurrentState;

        if (_rb != null && lastState != null)
        {
            _rb.position = lastState.Position;
            _rb.velocity = lastState.Velocity;
            _rb.rotation = lastState.Rotation; 
        }
        
    }

    /// <summary>
    /// Adapt object states to enable rewind
    /// </summary>
    public void EnableRewind()
    {
        _rewind = true;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    /// <summary>
    /// Adapt object states to disable rewind
    /// </summary>
    private void DisableRewind()
    {
        _rewind = false;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        
        freezeForceRecorder.UnFreeze();
    }
    
    /// <summary>
    /// Freezes all focused objects
    /// </summary>
    protected void Freeze()
    {
        _rewind = false;
        freezeForceRecorder.Freeze();
    }

    private void FixedUpdate()
    {
        // if rewind is not set to true, the state recording is active
        if (!_rewind)
        {
            stateRecorder.RecordStates();
        }
        else
        {
            timeReverser.Rewind();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // add force of every collision
        freezeForceRecorder.AddForce(collision.relativeVelocity);
    }

   
}
