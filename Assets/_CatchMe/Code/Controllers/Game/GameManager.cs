using UnityEngine;
using AlexDev.Networking;
using AlexDev.CatchMe.UI;
using UnityEngine.SceneManagement;

namespace AlexDev.CatchMe
{
    public class GameManager
    {
        #region Private Fields

        private RoomManager _roomManager;
        private MainMenuUI _mainMenuUI;
        private PlayersBase _playersBase;

        #endregion


        public GameManager(MainMenuUI mainMenuUI)
        {
            _mainMenuUI = mainMenuUI;
            _roomManager = new GameObject("RoomManager", typeof(RoomManager)).GetComponent<RoomManager>();
            _roomManager.AwakePhaseCompletedEvent += Initialize;
            SwitchToRoomUI();
        }

        #region Public Methods

        public void Initialize()
        {
            _roomManager.RoomLeftEvent += LoadMainMenu;
            _mainMenuUI.StartGameButtonPressedEvent += LoadGameScene;
            _mainMenuUI.LeaveRoomButtonPressedEvent += CloseRoomInMainMenu;
            if (GameUI.IsInitialized)
            {
                OnGameSceneLoaded();
            }
            else
            {
                GameUI.AwakePhaseCompletedEvent += OnGameSceneLoaded;
            }
        }

        public void LeaveRoom()
        {
            _roomManager.LeaveRoom();
        }

        public void LoadMainMenu()
        {
            _roomManager.LoadLevel("MainMenu");
            SelfDestroy();
        }

        public void LoadGameScene()
        {
            _roomManager.LoadLevel("Office");
        }

        #endregion

        #region Private Methods

        private void OnGameSceneLoaded()
        {
            SpawnManager.instance.SpawnPlayer(0);
            SubscribeForGameObjects();
        }

        private void SubscribeForGameObjects()
        {
            GameUI.instance.ExitButtonPressedEvent += LeaveRoom;
        }

        private void SwitchToRoomUI()
        {
            _mainMenuUI.ShowRoomUI();
        }

        private void CloseRoomInMainMenu()
        {
            _roomManager.LeaveRoom();
            SelfDestroy();
            SceneManager.LoadScene("MainMenu");
        }

        private void SelfDestroy()
        {
            Debug.Log(this + " SelfDestroing");
            if (GameUI.IsInitialized)
            {
                GameUI.instance.ExitButtonPressedEvent -= LeaveRoom;
            }
            GameUI.AwakePhaseCompletedEvent -= SubscribeForGameObjects;
            _roomManager.AwakePhaseCompletedEvent -= Initialize;
            _roomManager.RoomLeftEvent -= LoadMainMenu;
            _mainMenuUI.StartGameButtonPressedEvent -= LoadGameScene;
            _mainMenuUI.LeaveRoomButtonPressedEvent -= CloseRoomInMainMenu;
            _roomManager.SelfDestroy();
        }

        #endregion
    }
}
