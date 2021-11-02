using UnityEngine;


public class FreezeForceRecorder
{
    private Vector3 storedImpuls;
    private Rigidbody _rigidbody;

    public FreezeForceRecorder(Rigidbody rigidbody)
    {
        this._rigidbody = rigidbody;
        this.storedImpuls = new Vector3(0,0,0);
    }

    public void AddForce(Vector3 force)
    {
        storedImpuls += force;
    }
    
    public void Freeze()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }
    
    public void UnFreeze()
    {
        _rigidbody.AddForce(storedImpuls);
        storedImpuls = new Vector3(0, 0, 0);
    }
}
