using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class ControllerPUN: MonoBehaviourPun
{
    protected Rigidbody _rb;
    protected bool _rewind;
    
    protected StateCollectionPUN _statesOverTime;
    private TimeReverserPUN timeReverser;
    private StateRecorderPUN stateRecorder;
    private FreezeForceRecorderPUN freezeForceRecorder;


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
        StatePUN lastState = timeReverser.CurrentState;

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
        FreezeAll();
        photonView.RPC(nameof(FreezeAll), RpcTarget.All);        
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
        if (freezeForceRecorder != null)
        {
            freezeForceRecorder.AddForce(collision.relativeVelocity);
        }
    }
    

    protected virtual void FreezeAll()
    {
        _rewind = false;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

   
}
