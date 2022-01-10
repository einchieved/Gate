using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    private int level = 0;
    public GameObject timePlayerPrefab;
    public GameObject portalPlayerPrefab;

    private int playerCount = 0;

    private void Start()
    {
        if (timePlayerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                switch (PhotonNetwork.CurrentRoom.PlayerCount )
                {
                    case 2 : 
                        PhotonNetwork.Instantiate(this.timePlayerPrefab.name, new Vector3(24f, 1f, -12f), Quaternion.identity, 0);
                        break;
                    case 1 :
                        PhotonNetwork.Instantiate(this.portalPlayerPrefab.name, new Vector3(6f, 2f, -3f), Quaternion.identity, 0);
                        break;
                }
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
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

    void LoadNextLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.Name);

        string currentRoomName = PhotonNetwork.CurrentRoom.Name;
        
        PhotonNetwork.LoadLevel(currentRoomName.Substring(0, currentRoomName.Length - 2) + ++level);
    }
}
