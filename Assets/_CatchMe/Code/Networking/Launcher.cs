using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using AlexDev.Observer;
using System.Collections.Generic;

namespace AlexDev.Networking
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        private string gameVersion = "1";


        #endregion

        #region Public Fields

        public ObservableVariable<string> statusMessages;
        public bool IsConnectedToLobby { get; private set; } = false;

        #endregion

        #region Events

        public event Action<bool> ConnectionStatusChangedEvent;
        public event Action<List<RoomInfo>> RoomListUpdatedEvent;

        #endregion

        #region MonoBehaviour CallBacks

        void Awake()
        {
            statusMessages = new ObservableVariable<string>();
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            ConnectToMaster();
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            statusMessages.Value = "Connected to Lobby";
            IsConnectedToLobby = true;
            ConnectionStatusChangedEvent?.Invoke(IsConnectedToLobby);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            IsConnectedToLobby = false;
            ConnectionStatusChangedEvent?.Invoke(IsConnectedToLobby);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            statusMessages.Value = "<color=#ff0000ff>Can't join random room</color>";
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            statusMessages.Value = "Joined room";
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (roomList.Count == 0) return;
            RoomListUpdatedEvent?.Invoke(roomList);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the connection process.
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void ConnectToMaster()
        {
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                //PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public void SetPlayerNickName(string nickName)
        {
            PhotonNetwork.NickName = nickName;
        }

        public void JoinRandomRoom()
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }

        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom, IsOpen = true, PublishUserId = true });
        }

        #endregion

    }
}

