using System;
using UnityEngine;

namespace AlexDev.CatchMe.UI
{
    public class MainMenuUI : MonoBehaviour, IConnectionAware
    {
        #region Private Serializable Fields

        [SerializeField] private MenuPanelUI _menuPanelUI;
        [SerializeField] private SettingsPanelUI _settingsPanelUI;
        [SerializeField] private TextInputPanelUI _playerNamePanelUI;
        [SerializeField] private TextInputPanelUI _newRoomNamePanelUI;
        [SerializeField] private TextInputPanelUI _joinRoomNamePanelUI;
        [SerializeField] private RoomTableUI _roomTableUI;
        [SerializeField] private RoomMenuUI _roomMenuUI;

        #endregion
        #region Private Fields

        private GameObject _loadingScreen;

        #endregion

        #region Public Fields

        public SettingsPanelUI GetSettingsPanelUI => _settingsPanelUI;
        public RoomTableUI RoomTable => _roomTableUI;

        #endregion

        #region Events

        public event Action JoinRandomButtonPresedEvent;
        public event Action StartGameButtonPressedEvent;
        public event Action LeaveRoomButtonPressedEvent;

        public event Action<string> PlayerNameChangedEvent;
        public event Action<string> NewRoomNameEnteredEvent;
        public event Action<string> JoinRoomNameEnteredEvent;
        public event Action<bool> ConnectionStateChangedEvent;

        #endregion

        #region MonoBehaviour CallBacks

        private void Start()
        {
            _loadingScreen = NetworkStateUI.instance.GetLoadingScreen;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _newRoomNamePanelUI.OnConfirmingTextEvent += OnNewRoomNameEntered;
            _joinRoomNamePanelUI.OnConfirmingTextEvent += OnJoinRoomNameEntered;
        }

        private void OnDisable()
        {
            _newRoomNamePanelUI.OnConfirmingTextEvent -= OnNewRoomNameEntered;
        }

        #endregion

        #region Public methods

        public void OnConnectionChanged(bool isConnected)
        {
            throw new NotImplementedException();
        }

        public void OnJoinRoomNameEntered(string roomName)
        {
            JoinRoomNameEnteredEvent?.Invoke(roomName);
        }

        public void OnJoinRandomButton()
        {
            JoinRandomButtonPresedEvent?.Invoke();
        }

        public void OnStartGameButtonPressed()
        {
            StartGameButtonPressedEvent?.Invoke();
        }

        public void OnLeaveRoomButtonPressed()
        {
            LeaveRoomButtonPressedEvent?.Invoke();
        }

        public void ToggleInteractableAllGameButtons(bool isOn)
        {
            ToggleInteractableOfNewGameButton(isOn);
            ToggleInteractableOfjoinByIDButton(isOn);
            ToggleInteractableOfJoinRandomButton(isOn);
        }

        public void ToggleInteractableOfNewGameButton(bool isOn)
        {
            _menuPanelUI.ToggleInteractableOfNewGameButton(isOn);
        }

        public void ToggleInteractableOfjoinByIDButton(bool isOn)
        {
            _menuPanelUI.ToggleInteractableOfjoinByIDButton(isOn);
        }

        public void ToggleInteractableOfJoinRandomButton(bool isOn)
        {
            _menuPanelUI.ToggleInteractableOfJoinRandomButton(isOn);
        }

        public void ShowPlayerNameInputPanel(string currentName)
        {
            _playerNamePanelUI.SetPlaseholderText(currentName);
            _playerNamePanelUI.OnConfirmingTextEvent += PlayerNameChangedEvent;
            SwitchPanels(_menuPanelUI.gameObject, _playerNamePanelUI.transform.parent.gameObject);
        }

        public void ShowRoomUI()
        {
            _loadingScreen.SetActive(false);
            _roomMenuUI.gameObject.SetActive(true);
            _roomTableUI.gameObject.SetActive(true);
        }

        public void ShowMainMenuPanel()
        {
            _loadingScreen.SetActive(false);
            _menuPanelUI.gameObject.SetActive(true);
            _roomTableUI.gameObject.SetActive(true);
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
            HideAllPanels();
        }

        public void HideAllPanels()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        #endregion

        #region Private Methods

        private void SwitchPanels(GameObject fromPanel, GameObject toPanel)
        {
            fromPanel.SetActive(false);
            toPanel.SetActive(true);
        }

        private void OnNewRoomNameEntered(string roomName)
        {
            NewRoomNameEnteredEvent?.Invoke(roomName);
        }

        #endregion

    }
}
