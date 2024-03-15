using AlexDev.CatchMe.Data;
using AlexDev.CatchMe.Audio;
using UnityEngine;

namespace AlexDev.CatchMe
{
    public class AudioController
    {
        #region Private Fields

        private AudioManager _audioManager;
        private GameSettingsData _gameSettings;

        #endregion

        public AudioController(GameSettingsData gameSettingsData)
        {
            _audioManager = AudioManager.instance;
            _gameSettings = gameSettingsData;
            Initialize();
            PlayMenuMusic();
        }

        #region Private Fields

        private void Initialize()
        {
            _audioManager.SetMusicVolume(_gameSettings.musicVolume);
            _audioManager.SetSFXVolume(_gameSettings.sfxVolume);
            _audioManager.SwitchOnMusic(_gameSettings.isMusicOn);
            _audioManager.SwitchOnSfx(_gameSettings.isSfxOn);
        }

        #endregion

        #region Public Fields

        public void PlayMenuMusic()
        {
            _audioManager.PlayMusic("BGM0");
        }

        public void SetMusicVolume(float volume)
        {
            _audioManager.SetMusicVolume(volume);
            _gameSettings.musicVolume = volume;
        }

        public void SetSfxVolume(float volume)
        {
            _audioManager.SetSFXVolume(volume);
            _gameSettings.sfxVolume = volume;
        }

        public void SwitchOnMusic(bool isOn)
        {
            _audioManager.SwitchOnMusic(isOn);
            _gameSettings.isMusicOn = isOn;
            _audioManager.PlaySound("TAP0");
        }

        public void SwitchOnSfx(bool isOn)
        {
            _audioManager.SwitchOnSfx(isOn);
            _gameSettings.isSfxOn = isOn;
            if (isOn) _audioManager.PlaySound("TAP0");
        }

        #endregion
    }
}
