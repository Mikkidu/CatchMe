
using Photon.Pun;
using System;

namespace AlexDev.Networking
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {

        #region Events

        public event Action AwakePhaseCompletedEvent;
        public event Action RoomLeftEvent;
        public event Action RoomEnteredEvent;

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

        #endregion
    }
}
