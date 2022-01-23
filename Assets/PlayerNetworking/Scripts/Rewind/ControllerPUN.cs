using Photon.Pun;
using UnityEngine;

/// <summary>
/// Base class to enable time manipulation on the attached object
/// </summary>
public class ControllerPUN: MonoBehaviourPun
{
    protected Rigidbody _rb;
    protected bool _rewind;
    
    protected StateCollectionPUN _statesOverTime;
    private TimeReverserPUN timeReverser;
    private StateRecorderPUN stateRecorder;
    private FreezeForceRecorderPUN freezeForceRecorder;


    /// <summary>
    /// Creates all objects necessary to enable time manipulation on the attached object
    /// </summary>
    protected void InitStateHandling()
    {
        _statesOverTime = new StateCollectionPUN();
        
        timeReverser = new TimeReverserPUN(_rb, _statesOverTime);
        stateRecorder = new StateRecorderPUN(_rb, _statesOverTime);
        
        freezeForceRecorder = new FreezeForceRecorderPUN(_rb);
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
        
        // start Rewind
        if (Input.GetKeyDown(rewindKey))
        {
            EnableRewind();
        }

        // end Rewind
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
        StatePUN lastState = timeReverser.CurrentState;

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
        FreezeAll();
        photonView.RPC(nameof(FreezeAll), RpcTarget.All);        
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
        if (freezeForceRecorder != null)
        {
            freezeForceRecorder.AddForce(collision.relativeVelocity);
        }
    }
    
    /// <summary>
    /// Delete every recorded state data
    /// </summary>
    protected virtual void ResetAll()
    {
        _statesOverTime = new StateCollectionPUN();
        timeReverser = new TimeReverserPUN(_rb, _statesOverTime);
        stateRecorder = new StateRecorderPUN(_rb, _statesOverTime);
    }
    
    /// <summary>
    /// Adapt Object to enable freeze state
    /// </summary>
    protected virtual void FreezeAll()
    {
        _rewind = false;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

   
}
