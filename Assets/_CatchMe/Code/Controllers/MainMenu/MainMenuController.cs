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
            settingsUI.OnMusicToggleChangeEvent += _audioController.SwitchOnMusic;
            settingsUI.OnSfxToggleChangeEvent += _audioController.SwitchOnSfx;
            settingsUI.OnMusicVolumeChangeEvent += _audioController.SetMusicVolume;
            settingsUI.OnSfxVolumeChangeEvent += _audioController.SetSfxVolume;
            settingsUI.OnSaveSettingsEvent += _dataManager.SaveGameSettings;
            

            _mainMenuUI.NewGameButtonPressedEvent += _launcher.CreateRoom;
            _mainMenuUI.JoinRandomButtonPresedEvent += _launcher.JoinRandomRoom;

            _launcher.OnIsConnectedChangeEvent += OnIsConnectedChange;
            _launcher.SetPlayerNickName(_dataManager.playerSettings.playerName);
            MessageViewerUI.instance?.AddObservable(_launcher.statusMessages);

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

        #endregion

    }
}
