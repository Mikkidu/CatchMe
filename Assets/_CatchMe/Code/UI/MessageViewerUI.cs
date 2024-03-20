using UnityEngine;
using TMPro;
using AlexDev.Observer;
using System;

namespace AlexDev.CatchMe.UI
{
    public class MessageViewerUI : ObserverMonoBehaviour
    {
        #region Public Fields

        public static MessageViewerUI instance;

        #endregion

        #region Serialize PrivateFields

        [SerializeField] private TextMeshProUGUI _messagesText;

        #endregion

        #region MonoBehaviour Callbacks

        public void Awake()
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

        #region Protected Methods

        protected override void OnChanged(object o)
        {
            _messagesText.text = o.ToString();
        }

        #endregion
        
    }
}
