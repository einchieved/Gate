using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
        #region Private Serializable Fields

            [Tooltip("The Ui Panel to let the user enter name, connect and play")]
            [SerializeField]
            private GameObject controlPanel;
            [Tooltip("The UI Label to inform the user that the connection is in progress")]
            [SerializeField]
            private GameObject progressLabel;

        #endregion
        



        #region Private Fields


            /// <summary>
            /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
            /// </summary>
            string gameVersion = "1";
            
            bool isConnecting;
            
            [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
            [SerializeField]
            private byte maxPlayersPerRoom = 2;


        #endregion


        #region MonoBehaviour CallBacks


            /// <summary>
            /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
            /// </summary>
            void Awake()
            {
                PhotonNetwork.AutomaticallySyncScene = true;
            }

            private void Start()
            {
                progressLabel.SetActive(false);
                controlPanel.SetActive(true);
            }

        #endregion


        #region Public Methods


            /// <summary>
            /// Start the connection process.
            /// - If already connected, we attempt joining a random room
            /// - if not yet connected, Connect this application instance to Photon Cloud Network
            /// </summary>
            public void Connect()
            {
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


        #endregion
        
        #region MonoBehaviourPunCallbacks Callbacks


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
            
            Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PLauncher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            
            PhotonNetwork.LoadLevel("Room1");
        }


        #endregion
}
