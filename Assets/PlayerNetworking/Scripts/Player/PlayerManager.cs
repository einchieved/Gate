using Photon.Pun;
using UnityEngine;

/// <summary>
/// Makes sure only one local player instance is loaded
/// </summary>
public class PlayerManager : MonoBehaviourPunCallbacks
{

    public static GameObject LocalPlayerInstance;
    
    private void Awake()
    {
        // load only an instance if the photonview is mine
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        DontDestroyOnLoad(gameObject);
    }

}
