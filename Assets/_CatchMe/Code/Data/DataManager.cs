using System.IO;
using UnityEngine;


namespace AlexDev.CatchMe.Data
{


    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;

        #region PublicFields

        public GameSettingsData gameSettings { get; private set; }
        public PlayerSettingsData playerSettings { get; private set; }
        public bool isNewPlayerData = false;

        #endregion

        #region Private Fields

        private const string GAME_SETTINGS_FILE_NAME = "GameSettings";
        private const string PLAYER_SETTINGS_FILE_NAME = "PlayerSettings";

        #endregion

        #region Events



        #endregion

        #region Monobehaviour Callbacks

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameSettings();
            LoadPlayerSettings();
        }

        #endregion

        #region Public Methods

        public void SavePlayerSettings()
        {
            SaveData(PLAYER_SETTINGS_FILE_NAME, playerSettings);
        }

        public void SaveGameSettings()
        {
            SaveData(GAME_SETTINGS_FILE_NAME, gameSettings);
        }

        public void LoadPlayerSettings()
        {
            if (TryLoadData<PlayerSettingsData>(PLAYER_SETTINGS_FILE_NAME, out var loadedData))
            {
                playerSettings = loadedData;
            }
            else
            {
                playerSettings = new PlayerSettingsData();
                isNewPlayerData = true;
            }
        }

        public void LoadGameSettings()
        {
            if (TryLoadData<GameSettingsData>(GAME_SETTINGS_FILE_NAME, out var loadedData))
            {
                gameSettings = loadedData;
            }
            else
            {
                gameSettings = new GameSettingsData();
            }
        }

        #endregion

        #region Private Methods

        private void SaveData(string saveFileName, object dataObject)
        {
            string json = JsonUtility.ToJson(dataObject);
            File.WriteAllText(Application.persistentDataPath + $"/{saveFileName}.json", json);
        }

        private bool TryLoadData<T>(string saveFileName, out T dataObject) where T : class
        {
            dataObject = LoadData<T>(saveFileName);
            if (dataObject != null)
            {
                return true;
            }
            return false;
        }

        private T LoadData<T>(string saveFileName) where T : class
        {
            T loadedData;
            string path = Application.persistentDataPath + $"/{saveFileName}.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                loadedData = JsonUtility.FromJson<T>(json);
            }
            else
            {
                loadedData = null;
            }
            return loadedData;
        }

        #endregion



    }


}
