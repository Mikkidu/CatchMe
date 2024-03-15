using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.CatchMe.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        #region Private Serializable Fields

        [SerializeField] private MenuPanelUI _menuPanelUI;
        [SerializeField] private SettingsPanelUI _settingsPanelUI;
        [SerializeField] private TextInputPanelUI _playerNamePanelUI;

        #endregion

        #region Public Fields

        public SettingsPanelUI GetSettingsPanelUI => _settingsPanelUI;

        #endregion

        #region Events

        public event Action OnNewGameButtonUIEvent;
        public event Action OnJoinByIDButtonUIEvent;
        public event Action OnJoinRandomButtonUIEvent;

        public event Action<string> OnPlayerNameChangedEvent;

        #endregion


        #region Public methods

        public void OnNewGameButton()
        {
            OnNewGameButtonUIEvent?.Invoke();
        }

        public void OnJoinByIDButton()
        {
            OnJoinByIDButtonUIEvent?.Invoke();
        }

        public void OnJoinRandomButton()
        {
            OnJoinRandomButtonUIEvent?.Invoke();
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
            Debug.Log("Show input name");
            _playerNamePanelUI.SetPlaseholderText(currentName);
            _playerNamePanelUI.OnConfirmingTextEvent += OnPlayerNameChanged;
            SwitchPanels(_menuPanelUI.gameObject, _playerNamePanelUI.transform.parent.gameObject);
        }

        private void OnPlayerNameChanged(string newName)
        {
            OnPlayerNameChangedEvent?.Invoke(newName);
            _playerNamePanelUI.OnConfirmingTextEvent -= OnPlayerNameChanged;
            SwitchPanels(_playerNamePanelUI.transform.parent.gameObject, _menuPanelUI.gameObject);
        }

        #endregion

        #region Private Methods

        private void SwitchPanels(GameObject fromPanel, GameObject toPanel)
        {
            fromPanel.SetActive(false);
            toPanel.SetActive(true);
        }

        #endregion

    }
}
