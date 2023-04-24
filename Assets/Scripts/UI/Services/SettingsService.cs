using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace UI.Services
{
    public sealed class SettingsService : IInitializable, IDisposable
    {
        private const string MIXER_VOLUME = "Volume";
        private const int THRESHOLD_DB = 20;

        private readonly SettingsDataService _settingsDataService;
        private readonly SettingsWindowCanvasView _settingsWindowCanvasView;
        private readonly AudioMixerGroup _audioMixerGroup;

        private Resolution[] _resolutions;

        public SettingsService(
            SettingsDataService settingsDataService,
            SettingsWindowCanvasView settingsWindowCanvasView,
            AudioMixerGroup audioMixerGroup
            )
        {
            _settingsDataService = settingsDataService;
            _settingsWindowCanvasView = settingsWindowCanvasView;
            _audioMixerGroup = audioMixerGroup;
        }

        public void Initialize()
        {
            InitResolutionDropdown();

            var (isFullscreen, resolution, quality, volume) = _settingsDataService.GetSettings();

            _settingsWindowCanvasView.FullscreenToggle.isOn = isFullscreen;
            _settingsWindowCanvasView.ResolutionDropdown.value = GetResolutionValue(resolution);
            _settingsWindowCanvasView.QualityDropdown.value = quality;
            _settingsWindowCanvasView.VolumeSlider.value = GetCorrectVolumeSliderValue(volume, -80f, 0f, 0f, 1f);

            _settingsWindowCanvasView.ConfirmButton.onClick.AddListener(ConfirmSettings);
        }
        public void Dispose()
        {
            _settingsWindowCanvasView.ConfirmButton.onClick.RemoveListener(ConfirmSettings);
        }

        private void InitResolutionDropdown()
        {
            _settingsWindowCanvasView.ResolutionDropdown.ClearOptions();
            _resolutions = Screen.resolutions;
            var options = new List<string>();

            foreach (var resolution in _resolutions)
            {
                var option = $"{resolution.width}x{resolution.height} {resolution.refreshRate}Hz";
                options.Add(option);
            }

            _settingsWindowCanvasView.ResolutionDropdown.AddOptions(options);
        }

        private int GetResolutionValue(Resolution resolution)
        {
            for (int i = 0; i < _resolutions.Length; i++)
            {
                if (resolution.height == _resolutions[i].height && resolution.width == _resolutions[i].width)
                {
                    return i;
                }
            }

            return _resolutions.Length;
        }

        private float GetCorrectVolumeSliderValue(float value, float min1, float max1, float min2, float max2)
        {
            return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
        }

        private void ConfirmSettings()
        {
            var isFullscreen = _settingsWindowCanvasView.FullscreenToggle.isOn;
            var resolutionIndex = _settingsWindowCanvasView.ResolutionDropdown.value;
            var qualityIndex = _settingsWindowCanvasView.QualityDropdown.value;
            var volume = Mathf.Log10(_settingsWindowCanvasView.VolumeSlider.value) * THRESHOLD_DB;

            Screen.SetResolution(_resolutions[resolutionIndex].width, _resolutions[resolutionIndex].height, isFullscreen);
            QualitySettings.SetQualityLevel(qualityIndex);
            _audioMixerGroup.audioMixer.SetFloat(MIXER_VOLUME, volume);

            _settingsDataService.SetSettings(isFullscreen, _resolutions[resolutionIndex], qualityIndex, volume);
        }
    }
}
