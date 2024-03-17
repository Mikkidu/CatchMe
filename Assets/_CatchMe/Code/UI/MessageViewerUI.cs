using UnityEngine;
using TMPro;
using AlexDev.Observer;
using System;

namespace AlexDev.CatchMe.UI
{
    public class MessageViewerUI : ObserverMonoBehaviour
    {
        public static MessageViewerUI instance;

        [SerializeField] private TextMeshProUGUI _messagesText;

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

        protected override void OnChanged(object o)
        {
            _messagesText.text = o.ToString();
        }

        
    }
}
