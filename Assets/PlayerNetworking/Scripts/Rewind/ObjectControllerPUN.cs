using Photon.Pun;
using UnityEngine;

public class ObjectControllerPUN : ControllerPUN, ITimeControlablePUN
{
    private void Start()
    {
        GetRigidBody();
        InitStateHandling();
    }

    private void Update()
    {
        if (IsFocused)
        {
            Debug.Log("Focuuuus");
            HandleRewind(KeyCode.E, KeyCode.T);
            Debug.Log("isFocused");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            photonView.RPC(nameof(ResetAll), RpcTarget.All);
        }
    }

    [PunRPC]
    protected override void FreezeAll() {
        base.FreezeAll();
    }
    
    [PunRPC]
    private void ResetAll() {
        _statesOverTime.Reset();
    }
    
    public bool IsFocused { get; set; }
}