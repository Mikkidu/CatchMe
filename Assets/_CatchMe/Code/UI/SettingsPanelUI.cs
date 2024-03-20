using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlexDev.CatchMe
{
    public class SettingsPanelUI : MonoBehaviour
    {
        #region Serialize Private Fields

        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        #endregion

        #region Events

        public event Action<bool> MusicToggleChangedEvent;
        public event Action<bool> SfxToggleChangedEvent;
        public event Action<float> MusicVolumeChangedEvent;
        public event Action<float> SfxVolumeChangedEvent;
        public event Action SettingsSavedEvent;

        #endregion

        #region Public Methods

        public void Initialize(float musicVolume, float sfxVolume, bool isMusicOn, bool isSfxOn)
        {
            _musicSlider.value = musicVolume;
            _sfxSlider.value = sfxVolume;
            _musicToggle.isOn = isMusicOn;
            _sfxToggle.isOn = isSfxOn;
        }


        public void OnMusicToggleChange(bool isOn)
        {
            MusicToggleChangedEvent?.Invoke(isOn);
        }

        public void OnSfxToggleChange(bool isOn)
        {
            SfxToggleChangedEvent?.Invoke(isOn);
        }

        public void OnMusicVolumeChange(float volume)
        {
            MusicVolumeChangedEvent?.Invoke(volume);
        }


        public void OnSfxVolumeChange(float volume)
        {
            SfxVolumeChangedEvent?.Invoke(volume);
        }

        public void SaveSettings()
        {
            SettingsSavedEvent?.Invoke();
        }

        #endregion
    }
}
