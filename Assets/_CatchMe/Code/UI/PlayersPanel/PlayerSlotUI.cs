using UnityEngine;
using TMPro;

namespace AlexDev.CatchMe
{
    public class PlayerSlotUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _freeTimeText;
        [SerializeField] private TextMeshProUGUI _maxTiMeText;

        #endregion

        #region Public Fields

        private int _playerID;

        public int GetPlayerID => _playerID;

        #endregion

        #region Public Methods

        public void SetStats(int playerID, string name, int time)
        {
            _playerNameText.text = name;
            _freeTimeText.text = time.ToString();
            _playerID = playerID;
        }

        public void RefreshStats(int freePlayerTime, int maxFreeTime)
        {
            _freeTimeText.text = freePlayerTime.ToString();
            _maxTiMeText.text = maxFreeTime.ToString();
        }

        public void RemovePlayer()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
