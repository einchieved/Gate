using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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
            
            public void Connect()
            {
                Cursor.lockState = CursorLockMode.Locked;
                progressLabel.SetActive(true);
                controlPanel.SetActive(false);
                
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
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");

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
