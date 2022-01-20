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
            HandleRewind(KeyCode.E, KeyCode.T);
            //Debug.Log("isFocused");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            IsFocused = false;
        }

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