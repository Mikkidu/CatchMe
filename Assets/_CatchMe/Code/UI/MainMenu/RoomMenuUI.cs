using UnityEngine;
using UnityEngine.Events;

namespace AlexDev.CatchMe
{
    public class RoomMenuUI : MonoBehaviour
    {

        #region Events

        public UnityEvent StartGameButtonPressedEvent;
        public UnityEvent LeaveRoomButtonPressedEvent;

        #endregion

        #region Public Methods

        public void OnStartGameButtonPressed()
        {
            StartGameButtonPressedEvent?.Invoke();
        }

        public void OnLeaveRoomButtonPressed()
        {
            LeaveRoomButtonPressedEvent?.Invoke();
        }

        #endregion

    }
}
