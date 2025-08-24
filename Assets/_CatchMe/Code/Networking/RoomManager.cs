
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace AlexDev.Networking
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {

        #region Events

        public event Action AwakePhaseCompletedEvent;
        public event Action RoomLeftEvent;
        public event Action RoomEnteredEvent;
        public event Action<Player> PlayerEnteredRoomEvent;
        public event Action<int> PlayerLeftRoomEvent;

        #endregion

        #region MonoBehaviour CallBack

        private void Start()
        {
            AwakePhaseCompletedEvent?.Invoke();
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region MonoBehaviourPunCallbacks

        public override void OnLeftRoom()
        {
            RoomLeftEvent?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            RoomEnteredEvent?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            PlayerEnteredRoomEvent?.Invoke(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerLeftRoomEvent?.Invoke(otherPlayer.ActorNumber);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {

        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void LoadLevel(string sceneName)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public Player[] GetPlayersInRoom()
        {
            return PhotonNetwork.PlayerList;
        }

        #endregion
    }
}
