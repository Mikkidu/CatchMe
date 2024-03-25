using System;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using AlexDev.Observer;
using UnityEngine.Events;

namespace AlexDev.Networking
{
    public class ConnectionManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        public static ConnectionManager Instance;

        public UnityEvent<bool> MasterConnectionChangeEvent;
        public ObservableVariable<string> statusMessages;

        public bool IsConnectedToMaster
        {
            get { return _isConnectedToMaster; }
            set
            {
                if (value == _isConnectedToMaster)
                    return;
                _isConnectedToMaster = value;
                if (MasterConnectionChangeEvent != null)
                    MasterConnectionChangeEvent.Invoke(_isConnectedToMaster);
                string message;
                if (_isConnectedToMaster)
                    message = "<color=green>Succsessfully connected to the MasterServer</color>";
                else
                    message = "<color=red>Server connection error</color>";
                statusMessages.Value = message;
            }
        }

        #endregion

        #region Private Fields

        private bool _isConnectedToMaster = false;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            statusMessages = new ObservableVariable<string>();
        }

        #endregion

        #region Pun CallBacks

        public override void OnConnectedToMaster()
        {
            statusMessages.Value = "Connected to Master";
            IsConnectedToMaster = true;
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            statusMessages.Value = $"Disconected from master with reason {cause}";
            IsConnectedToMaster = false;
        }

        #endregion

        #region Public Methods

        public void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
            statusMessages.Value =  "Connecting to masterServer";
        }

        #endregion
    }
}
