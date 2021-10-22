using DefaultNamespace;
using UnityEngine;

public class Controller: MonoBehaviour
{
    protected Rigidbody _rb;
    private bool _rewind;
    
    private StateCollection _statesOverTime;
    private TimeReverser timeReverser;
    private StateRecorder stateRecorder;

    protected void InitStateHandling()
    {
        _statesOverTime = new StateCollection();
        
        timeReverser = new TimeReverser(_rb, _statesOverTime);
        stateRecorder = new StateRecorder(_rb, _statesOverTime);
    }

    protected void GetRigidBody()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    protected void HandleRewind(KeyCode controllElement)
    {
        if (Input.GetKeyDown(controllElement))
        {
            EnableRewind();
        }

        if (Input.GetKeyUp(controllElement))
        {
            DisableRewind();
            AdaptState();
        }
    }
    

    private void AdaptState()
    {
        State lastState = timeReverser.CurrentState;
            
        _rb.position = lastState.Position;
        _rb.velocity = lastState.Velocity;
        _rb.rotation = lastState.Rotation;
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
    
}
