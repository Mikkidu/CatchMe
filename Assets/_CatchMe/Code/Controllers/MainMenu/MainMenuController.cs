using AlexDev.CatchMe.UI;
using AlexDev.Networking;
using AlexDev.CatchMe.Data;
using UnityEngine;

namespace AlexDev.CatchMe
{
    public class MainMenuController
    {
        #region Private Fields

        private MainMenuUI _mainMenuUI;
        private Launcher _launcher;
        private DataManager _dataManager;
        private AudioController _audioController;
        private RoomsBase _roomsBase;

        #endregion

        public MainMenuController( MainMenuUI mainMenuUI, Launcher launcher)
        {
            _mainMenuUI = mainMenuUI;
            _launcher = launcher;
        }

        #region Public Methods

        public void Initialize()
        {
            _dataManager = DataManager.instance;
            GameSettingsData gameSettings = _dataManager.gameSettings;

            _audioController = new AudioController(_dataManager.gameSettings);

            SettingsPanelUI settingsUI = _mainMenuUI.GetSettingsPanelUI;

            settingsUI.Initialize(
                gameSettings.musicVolume,
                gameSettings.sfxVolume,
                gameSettings.isMusicOn,
                gameSettings.isSfxOn);

            settingsUI.MusicToggleChangedEvent += _audioController.SwitchOnMusic;
            settingsUI.SfxToggleChangedEvent += _audioController.SwitchOnSfx;
            settingsUI.MusicVolumeChangedEvent += _audioController.SetMusicVolume;
            settingsUI.SfxVolumeChangedEvent += _audioController.SetSfxVolume;
            settingsUI.SettingsSavedEvent += _dataManager.SaveGameSettings;

            _mainMenuUI.RoomNameEnteredEvent += CreateRoom;
            _mainMenuUI.JoinRandomButtonPresedEvent += _launcher.JoinRandomRoom;

            _roomsBase = new RoomsBase();
            var roomsTableUI = _mainMenuUI.RoomTable;
            _roomsBase.NewRoomCreatedEvent += roomsTableUI.AddRoom;
            _roomsBase.RoomStateUpdatedEvent += roomsTableUI.RefreshRoom;
            _roomsBase.RoomRemovedEvent+= roomsTableUI.RemoveRoom;

            _launcher.ConnectionStatusChangedEvent += OnIsConnectedChange;
            _launcher.RoomListUpdatedEvent += _roomsBase.RefreshRoomList;
            _launcher.SetPlayerNickName(_dataManager.playerSettings.playerName);
            MessageViewerUI.instance?.AddObservableVariable(_launcher.statusMessages);


            if (_dataManager.isNewPlayerData)
            {
                _mainMenuUI.PlayerNameChangedEvent += ChangePlayerName;
                _mainMenuUI.ShowPlayerNameInputPanel(_dataManager.playerSettings.playerName);
            }
        }

        #endregion

        #region Private Methods

        private void OnIsConnectedChange(bool isConnected)
        {
            _mainMenuUI.ToggleInteractableAllGameButtons(isConnected);

        }

        private void ChangePlayerName(string newName)
        {
            _dataManager.playerSettings.playerName = newName;
            _launcher.SetPlayerNickName(newName);
            _dataManager.SavePlayerSettings();
        }

        private void CreateRoom(string roomaName)
        {
            Debug.Log("Main menu controller: create room");
            _launcher.CreateRoom(roomaName);
        }

        #endregion

    }
}
