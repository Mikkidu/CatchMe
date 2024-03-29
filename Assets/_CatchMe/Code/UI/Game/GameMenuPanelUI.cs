using UnityEngine;
using UnityEngine.Events;

namespace AlexDev.CatchMe
{
    public class GameMenuPanelUI : MonoBehaviour
    {

        public UnityEvent OnExitButtonPressed;

        public void CloseGameMenu()
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }

        public void OnExitGameButtonPressed()
        {
            OnExitButtonPressed?.Invoke();
        }

    }
}
