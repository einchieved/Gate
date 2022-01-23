using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    private int level = 1;
    public GameObject timePlayerPrefab;
    public GameObject portalPlayerPrefab;
    public GameObject p1SpawnPoint;
    public GameObject p2SpawnPoint;


    private int playerCount = 0;

    private void Start()
    {
        if (timePlayerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        else
        {
            // create only a player, when there is no other local instance
            if (PlayerManager.LocalPlayerInstance == null)
            {
               createPlayer(); 
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
    }
    
    /// <summary>
    /// Instantiate Player at the provided spawn point
    /// </summary>
    private void createPlayer()
    {
        switch (PhotonNetwork.CurrentRoom.PlayerCount )
        {
            case 2 :
                PhotonNetwork.Instantiate(portalPlayerPrefab.name, p2SpawnPoint.transform.position, Quaternion.identity, 0);
                break;
            case 1 :
                PhotonNetwork.Instantiate(timePlayerPrefab.name , p1SpawnPoint.transform.position, Quaternion.identity, 0);
                break;
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
