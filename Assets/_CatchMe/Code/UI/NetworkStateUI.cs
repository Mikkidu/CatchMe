using UnityEngine;
using TMPro;
using AlexDev.Observer;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AlexDev.CatchMe.UI
{
    public class NetworkStateUI : ObserverMonoBehaviour
    {
        #region Public Fields

        public static NetworkStateUI instance;

        public UnityEvent ReconnectButtonPressedEvent;

        #endregion

        #region Serialize PrivateFields

        [SerializeField] private TextMeshProUGUI _messagesText;
        [SerializeField] private GameObject _reconnectButton;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Public Methods

        public void ShowReconnectButton(bool isConnected)
        {
            if (_reconnectButton != null & _reconnectButton.activeSelf == isConnected)
            {
                _reconnectButton.GetComponent<Button>().interactable = true;
                _reconnectButton.SetActive(!isConnected);
            }
        }

        public void OnReconnectButtonPressed()
        {
            ReconnectButtonPressedEvent?.Invoke();
            _reconnectButton.GetComponent<Button>().interactable = false;
        }

        #endregion

        #region Protected Methods

        protected override void OnChanged(object o)
        {
            _messagesText.text = o.ToString();
        }

        #endregion
        
    }
}
