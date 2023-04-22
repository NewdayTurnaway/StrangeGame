using Gameplay.Input;
using Services;
using System;
using Zenject;

namespace UI.Services
{
    public sealed class PauseService : IInitializable, IDisposable
    {
        private readonly GameStateService _gameStateService;
        private readonly PlayerDataService _playerDataService;
        private readonly PlayerInput _playerInput;
        private readonly PauseCanvasView _pauseCanvasView;
        private readonly SettingsWindowCanvasView _settingsWindowCanvasView;

        private bool _isPause;

        public PauseService(
            GameStateService gameStateService,
            PlayerDataService playerDataService,
            PlayerInput playerInput,
            PauseCanvasView pauseCanvasView,
            SettingsWindowCanvasView settingsWindowCanvasView
            )
        {
            _gameStateService = gameStateService;
            _playerDataService = playerDataService;
            _playerInput = playerInput;
            _pauseCanvasView = pauseCanvasView;
            _settingsWindowCanvasView = settingsWindowCanvasView;
        }

        public void Initialize()
        {
            _pauseCanvasView.Init(OpenSettingsWindow, _gameStateService.GoToMenu, _gameStateService.ExitGame);
            _pauseCanvasView.ShowCanvas(false);

            _playerInput.PauseInput += OpenPauseWindow;
        }

        public void Dispose()
        {
            _playerInput.PauseInput -= OpenPauseWindow;
        }

        private void OpenPauseWindow(bool pauseInput)
        {
            if (_settingsWindowCanvasView == null) return;
            if (_settingsWindowCanvasView.IsEnabled) return;

            if (pauseInput)
            {
                _isPause = !_isPause;
                _pauseCanvasView.ShowCanvas(_isPause);
            }

            if (_pauseCanvasView.IsEnabled != _playerInput.IsPause)
            {
                _playerInput.IsPause = _pauseCanvasView.IsEnabled;
                _isPause = _playerInput.IsPause;
            }
        }
        
        private void OpenSettingsWindow()
        {
            _settingsWindowCanvasView.ShowCanvas(true);
        }
    }
}
