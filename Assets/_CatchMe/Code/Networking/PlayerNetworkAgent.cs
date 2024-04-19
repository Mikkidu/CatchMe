using Photon.Pun;
using UnityEngine;

namespace AlexDev.CatchMe
{
    public class PlayerNetworkAgent : MonoBehaviourPun
    {
        [Tooltip("This components will be disable on player's avatars")]
        [SerializeField] private MonoBehaviour[] _controlComponents;


        private void Awake()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected == true)
            {
                foreach (var component in _controlComponents)
                {
                    component.enabled = false;
                }
            }
        }

        void Start()
        {
        
        }


        void Update()
        {
        
        }
    }
}
