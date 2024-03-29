using UnityEngine;
using TMPro;
using AlexDev.Observer;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

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
                _reconnectButton.SetActive(!isConnected);
            }
        }

        public void OnReconnectButtonPressed()
        {
            ReconnectButtonPressedEvent?.Invoke();
            StartCoroutine(ReconnectColdown());
        }

        #endregion


        #region Private Methods

        private IEnumerator ReconnectColdown()
        {
            var button = _reconnectButton.GetComponent<Button>();
            button.interactable = false;
            yield return new WaitForSeconds(5);
            button.interactable = true;
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
