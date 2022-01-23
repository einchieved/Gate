using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Connects player which want to play the game
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    string gameVersion = "1";
    bool isConnecting;


    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    
    /// <summary>
    /// Join random room on method invocation
    /// </summary>
    public void Connect()
    {
        Cursor.lockState = CursorLockMode.Locked;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        
        // if the user is connected join within a room
        // else prepare connection set up
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    
    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TheChoiceIsYours");
    }
}
