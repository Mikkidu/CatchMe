using UnityEngine;
using TMPro;

namespace AlexDev.CatchMe.UI
{
    public class RoomSlotUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private TextMeshProUGUI _roomNameText;
        [SerializeField] private TextMeshProUGUI _playersCountText;

        #endregion

        #region Public Fields

        private int _roomIndex;
        
        public int GetRoomIndex => _roomIndex;

        #endregion

        #region Public Methods

        public void SetStats(int roomIndex, string name, int playersCount)
        {
            _roomNameText.text = name;
            _playersCountText.text = playersCount.ToString();
            _roomIndex = roomIndex;
        }

        public void RefreshStats(int playersCount)
        {
            _playersCountText.text = playersCount.ToString();
        }

        public void RemoveRoom()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
