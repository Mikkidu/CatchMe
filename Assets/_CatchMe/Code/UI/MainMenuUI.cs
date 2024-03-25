using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.CatchMe.UI
{
    public class MainMenuUI : MonoBehaviour, IConnectionAware
    {
        #region Private Serializable Fields

        [SerializeField] private MenuPanelUI _menuPanelUI;
        [SerializeField] private SettingsPanelUI _settingsPanelUI;
        [SerializeField] private TextInputPanelUI _playerNamePanelUI;
        [SerializeField] private TextInputPanelUI _roomNamePanelUI;
        [SerializeField] private RoomTableUI _roomTableUI;

        #endregion

        #region Public Fields

        public SettingsPanelUI GetSettingsPanelUI => _settingsPanelUI;
        public RoomTableUI RoomTable => _roomTableUI;

        #endregion

        #region Events

        public event Action JoinByIDButtonPressedEvent;
        public event Action JoinRandomButtonPresedEvent;

        public event Action<string> PlayerNameChangedEvent;
        public event Action<string> RoomNameEnteredEvent;
        public event Action<bool> ConnectionStateChangedEvent;

        #endregion

        #region MonoBehaviour CallBacks

        private void Start()
        {
            _roomNamePanelUI.OnConfirmingTextEvent += OnRoomNameEntered;
        }

        private void OnDisable()
        {
            _roomNamePanelUI.OnConfirmingTextEvent -= OnRoomNameEntered;
        }

        #endregion

        #region Public methods

        public void OnConnectionChanged(bool isConnected)
        {
            throw new NotImplementedException();
        }

        public void OnJoinByIDButton()
        {
            JoinByIDButtonPressedEvent?.Invoke();
        }

        public void OnJoinRandomButton()
        {
            JoinRandomButtonPresedEvent?.Invoke();
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

        #endregion

        #region Private Methods

        private void SwitchPanels(GameObject fromPanel, GameObject toPanel)
        {
            fromPanel.SetActive(false);
            toPanel.SetActive(true);
        }

        private void OnRoomNameEntered(string roomName)
        {
            RoomNameEnteredEvent?.Invoke(roomName);
        }

        #endregion

    }
}
