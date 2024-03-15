using UnityEngine;
using AlexDev.CatchMe.UI;
using AlexDev.CatchMe.Networking;

namespace AlexDev.CatchMe
{
    public class MenuEntryPoint : MonoBehaviour
    {
        #region Private Serializable Fields

        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Launcher _launcher;

        #endregion

        #region Readonly Fields

        public MainMenuController menuController { get; private set; }

        #endregion

        #region Private Methods



        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            menuController = new MainMenuController(_mainMenuUI, _launcher);
        }

        void Start()
        {
            menuController.Initialize();
        }

        #endregion

    }
}
