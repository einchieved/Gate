using DefaultNamespace;
using UnityEngine;

public class ControllerPUN: MonoBehaviour
{
    protected Rigidbody _rb;
    protected bool _rewind;
    
    private StateCollection _statesOverTime;
    private TimeReverser timeReverser;
    private StateRecorder stateRecorder;
    private FreezeForceRecorder freezeForceRecorder;


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
    
    protected void HandleRewind(KeyCode rewindKey, KeyCode endRewindKey)
    {
        if (Input.GetKeyDown(rewindKey))
        {
            EnableRewind();
        }

        if (Input.GetKeyDown(endRewindKey))
        {
            DisableRewind();
            AdaptState();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Freeze();
        }
    }
    

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

    public void EnableRewind()
    {
        _rewind = true;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    private void DisableRewind()
    {
        _rewind = false;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        
        freezeForceRecorder.UnFreeze();
    }
    
    protected void Freeze()
    {
        _rewind = false;
        freezeForceRecorder.Freeze();
    }

    private void FixedUpdate()
    {
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
        freezeForceRecorder.AddForce(collision.relativeVelocity);
    }

   
}
