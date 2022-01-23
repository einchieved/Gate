using UnityEngine;

/// <summary>
/// Records forces applied during the freeze state
/// </summary>
public class FreezeForceRecorderPUN
{
    private Vector3 storedImpuls;
    private Rigidbody _rigidbody;

    public FreezeForceRecorderPUN(Rigidbody rigidbody)
    {
        this._rigidbody = rigidbody;
        this.storedImpuls = new Vector3(0,0,0);
    }

    /// <summary>
    /// Stores received forced to retrive it after the freeze state ends
    /// </summary>
    /// <param name="force">stored force</param>
    public void AddForce(Vector3 force)
    {
        storedImpuls += force * 3;
    }
    
    /// <summary>
    /// Ends the freeze state and releases the stored force
    /// </summary>
    public void UnFreeze()
    {
        _rigidbody.AddForce(storedImpuls);
        storedImpuls = new Vector3(0, 0, 0);
    }
}
