using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.CatchMe.UI
{
    public class MenuPanelUI : MonoBehaviour
    {

        #region Private Serialize Fields

            [SerializeField] private Button _newGameButton;
            [SerializeField] private Button _joinByIDButton;
            [SerializeField] private Button _joinRandomButton;

        #endregion

        #region Private Fields

        private MainMenuUI _mainMenuUI;

        #endregion

        #region Public Methods

        public void Initialize(MainMenuUI mainMenuUI)
        {
            _mainMenuUI = mainMenuUI;
        }

        public void OnNewGameButton()
        {
            _mainMenuUI.OnNewGameButton();
        }

        public void OnJoinByIDButton()
        {
            _mainMenuUI.OnJoinByIDButton();
        }

        public void OnJoinRandomButton()
        {
            _mainMenuUI.OnJoinRandomButton();
        }



        #endregion

        #region Private Methods

        public void ToggleInteractableOfNewGameButton(bool isOn)
        {
            if (_newGameButton == null) return;

            _newGameButton.interactable = isOn;
        }

        public void ToggleInteractableOfjoinByIDButton(bool isOn)
        {
            if (_joinByIDButton == null) return;

            _joinByIDButton.interactable = isOn;
        }

        public void ToggleInteractableOfJoinRandomButton(bool isOn)
        {
            if (_joinRandomButton == null) return;

            _joinRandomButton.interactable = isOn;
        }

        #endregion

    }
}
