using System;
using UnityEngine;
using UnityEngine.Audio;


namespace AlexDev.CatchMe.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Public Fields

        public static AudioManager instance;
        public const string SETTINGS_VOLUME_MUSIC = "MusicVolume";
        public const string SETTINGS_VOLUME_SFX = "SfxVolume";

        #endregion

        #region Private Serialize Fields

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] public Sound[] sounds;
        [SerializeField] private Sound[] tracks;

        #endregion

        #region Private Fields

        private bool isMusicOn = true;
        private bool isSfxOn = true;
        private float _currentMusicVolume;
        private float _currentSfxVolume;

        #endregion

        #region Monobehaviour Callbacks
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Publick Methods

        public void InitializeVolume(float musicVolume, bool isMusicOn, float sfxVolume, bool isSfxOn)
        {
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
            SwitchOnMusic(isMusicOn);
            SwitchOnSfx(isSfxOn);
        }

        private void PlayMusic(Sound track)
        {
            musicSource.clip = track.clip;
            musicSource.Play();
        }

        public void PlayMusic(string trackName)
        {
            Sound track = Array.Find(tracks, tracks => tracks.Name == trackName);
            PlayMusic(track);
        }

        public void PlayMusicIfAnother(string trackName)
        {
            Sound track = Array.Find(tracks, tracks => tracks.Name == trackName);
            if (track.clip == musicSource.clip)
                return;
            PlayMusic(track);
        }

        public void PlaySound(string soundName)
        {
            Sound sound = Array.Find(sounds, sounds => sounds.Name == soundName);
            if (sound != null)
                sfxSource.PlayOneShot(sound.clip, sound.volume);
        }

        public void SetMusicVolume(float volume)
        {
            _currentMusicVolume = volume;
            if (!isMusicOn) return;
            _mixer.SetFloat(SETTINGS_VOLUME_MUSIC, Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume(float volume)
        {
            _currentSfxVolume = volume;
            if (!isSfxOn) return;
            _mixer.SetFloat(SETTINGS_VOLUME_SFX, Mathf.Log10(volume) * 20);
        }

        public void SwitchOnMusic(bool isOn)
        {
            isMusicOn = isOn;
            _mixer.SetFloat(SETTINGS_VOLUME_MUSIC, Mathf.Log10(_currentMusicVolume * (isOn ? 1 : 0.001f)) * 20);
        }

        public void SwitchOnSfx(bool isOn)
        {
            isSfxOn = isOn;
            _mixer.SetFloat(SETTINGS_VOLUME_SFX, Mathf.Log10(_currentSfxVolume * (isOn ? 1 : 0.001f)) * 20);
        }

        #endregion
    }
}
