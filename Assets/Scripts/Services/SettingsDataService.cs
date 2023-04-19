using System;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Services
{
    public sealed class SettingsDataService : IInitializable, IDisposable
    {
        private const string FULLSCREEN = "SettingsFullscreen";
        private const string RESOLUTION = "SettingsResolution";
        private const string QUALITY = "SettingsQuality";
        private const string VOLUME = "SettingsVolume";
        private const string MIXER_VOLUME = "Volume";

        private readonly AudioMixerGroup _audioMixerGroup;

        private bool _settingsFullscreen;
        private string _settingsResolution;
        private int _settingsQuality;
        private float _settingsVolume;

        public SettingsDataService(AudioMixerGroup audioMixerGroup)
        {
            _audioMixerGroup = audioMixerGroup;
        }

        public void Initialize()
        {
            if (!PlayerPrefs.HasKey(FULLSCREEN))
            {
                var value = Screen.fullScreen ? 1 : 0;
                PlayerPrefs.SetInt(FULLSCREEN, value);
            }

            if (!PlayerPrefs.HasKey(RESOLUTION))
            {
                var width = Screen.currentResolution.width;
                var height = Screen.currentResolution.height;
                var value = $"{width}x{height}";
                PlayerPrefs.SetString(RESOLUTION, value);
            }

            if (!PlayerPrefs.HasKey(QUALITY))
            {
                var value = QualitySettings.GetQualityLevel();
                PlayerPrefs.SetInt(QUALITY, value);
            }

            if (!PlayerPrefs.HasKey(VOLUME))
            {
                _audioMixerGroup.audioMixer.GetFloat(MIXER_VOLUME, out var value);
                PlayerPrefs.SetFloat(VOLUME, value);
            }

            LoadSettingsPrefs();
        }

        public void Dispose()
        {
            SaveSettingsPrefs();
        }

        public (bool, Resolution, int, float) GetSettings()
        {
            var resolutionStrings = _settingsResolution.Split('x');
            var resolution = new Resolution() 
            {
                width = Convert.ToInt32(resolutionStrings[0]),
                height = Convert.ToInt32(resolutionStrings[1]),
            };
            return (_settingsFullscreen, resolution, _settingsQuality, _settingsVolume);
        }

        public void SetSettings(bool isFullscreen, Resolution resolution, int quality, float volume)
        {
            var width = resolution.width;
            var height = resolution.height;
            var resolutionValue = $"{width}x{height}";
            
            _settingsFullscreen = isFullscreen;
            _settingsResolution = resolutionValue;
            _settingsQuality = quality;
            _settingsVolume = volume;

            SaveSettingsPrefs();
        }

        private void LoadSettingsPrefs()
        {
            _settingsFullscreen = PlayerPrefs.GetInt(FULLSCREEN) == 1;
            _settingsResolution = PlayerPrefs.GetString(RESOLUTION);
            _settingsQuality = PlayerPrefs.GetInt(QUALITY);
            _settingsVolume = PlayerPrefs.GetFloat(VOLUME);
        }

        private void SaveSettingsPrefs()
        {
            var isFullscreen = _settingsFullscreen ? 1 : 0; 
            PlayerPrefs.SetInt(FULLSCREEN, isFullscreen);
            PlayerPrefs.SetString(RESOLUTION, _settingsResolution);
            PlayerPrefs.SetInt(QUALITY, _settingsQuality);
            PlayerPrefs.SetFloat(VOLUME, _settingsVolume);
        }
    }
}