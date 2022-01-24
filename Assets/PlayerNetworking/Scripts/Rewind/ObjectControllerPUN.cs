using Photon.Pun;
using UnityEngine;

/// <summary>
/// Controller handles time manipulation on specific inputs
/// </summary>
public class ObjectControllerPUN : ControllerPUN, ITimeControlablePUN
{
    private void Start()
    {
        GetRigidBody();
        InitStateHandling();
    }

    private void Update()
    {
        
        // if the object is focused rewind is with the specified keys possible
        if (IsFocused)
        {
            HandleRewind(KeyCode.E, KeyCode.T);
            //Debug.Log("isFocused");
        }
        
        // removes the focus from one object to allow the focus of other objects
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }
        
        // Delete all recorded states and forces from all objects
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ResetAll();
            photonView.RPC(nameof(ResetAll), RpcTarget.All);
        }
    }

    [PunRPC]
    protected override void FreezeAll() {
        base.FreezeAll();
    }
    
    [PunRPC]
    protected override void ResetAll() {
        base.ResetAll();
    }
    
    public bool IsFocused { get; set; }
}