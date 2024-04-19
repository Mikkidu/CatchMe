using Photon.Pun;
using UnityEngine;

namespace AlexDev.CatchMe
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Fields

        public static SpawnManager instance;

        #endregion


        #region Serialize Pivate Fields

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _botPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        #endregion

        #region Public Methods

        public void SpawnPlayer(int spawnPointNumber)
        {
            if (_playerPrefab == null)
            {
                Debug.Log(name + " error. No player prefab");
            }
            else
            {
                PhotonNetwork.Instantiate(_playerPrefab.name, _spawnPoints[spawnPointNumber].position, Quaternion.identity);

            }
        }

        #endregion
        
    }
}
