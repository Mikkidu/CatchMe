using System;
using UnityEngine;

namespace AlexDev.CatchMe
{
    public class GameUI : MonoBehaviour
    {

        #region Public Fields

        public static GameUI instance;
        public static bool IsInitialized
        {
            get
            {
                if (instance != null) return true;
                return false;
            }
        }

        #endregion

        #region Serialize Private Fields

        [SerializeField] private GameMenuPanelUI _gamePanelUI;

        #endregion

        #region Private Fields

        #endregion

        #region Events

        public static event Action AwakePhaseCompletedEvent;
        public event Action ExitButtonPressedEvent;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Locked");
        }

        private void Start()
        {
            AwakePhaseCompletedEvent?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowGameMenu();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        #endregion

        #region Public Methods

        public void OnExitButtonPressed()
        {
            ExitButtonPressedEvent?.Invoke();
        }

        #endregion

        #region Private Methods

        private void ShowGameMenu()
        {
            _gamePanelUI.gameObject.SetActive(true);
        }

        #endregion


    }
}
