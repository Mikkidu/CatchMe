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

        public event Action<bool> OnMusicToggleChangeEvent;
        public event Action<bool> OnSfxToggleChangeEvent;
        public event Action<float> OnMusicVolumeChangeEvent;
        public event Action<float> OnSfxVolumeChangeEvent;
        public event Action OnSaveSettingsEvent;

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
            OnMusicToggleChangeEvent?.Invoke(isOn);
        }

        public void OnSfxToggleChange(bool isOn)
        {
            OnSfxToggleChangeEvent?.Invoke(isOn);
        }

        public void OnMusicVolumeChange(float volume)
        {
            OnMusicVolumeChangeEvent?.Invoke(volume);
        }


        public void OnSfxVolumeChange(float volume)
        {
            OnSfxVolumeChangeEvent?.Invoke(volume);
        }

        public void SaveSettings()
        {
            OnSaveSettingsEvent?.Invoke();
        }

        #endregion
    }
}
