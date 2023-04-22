using Gameplay.Input;
using Services;
using System;
using Zenject;

namespace UI.Services
{
    public sealed class PauseService : IInitializable, IDisposable
    {
        private readonly GameStateService _gameStateService;
        private readonly PlayerInput _playerInput;
        private readonly StatisticService _statisticService;
        private readonly PauseCanvasView _pauseCanvasView;
        private readonly SettingsWindowCanvasView _settingsWindowCanvasView;

        private bool _isPause;

        public PauseService(
            GameStateService gameStateService,
            PlayerInput playerInput,
            StatisticServiceFactory statisticServiceFactory,
            PauseCanvasView pauseCanvasView,
            SettingsWindowCanvasView settingsWindowCanvasView
            )
        {
            _gameStateService = gameStateService;
            _playerInput = playerInput;
            _pauseCanvasView = pauseCanvasView;
            _settingsWindowCanvasView = settingsWindowCanvasView;
            
            _statisticService = statisticServiceFactory.Create(_pauseCanvasView.LevelStatsView);
        }

        public void Initialize()
        {
            _pauseCanvasView.ShowCanvas(false);
            _pauseCanvasView.Init(OpenSettingsWindow, _gameStateService.GoToMenu, _gameStateService.ExitGame);

            _playerInput.PauseInput += OpenPauseWindow;
        }

        public void Dispose()
        {
            _statisticService.Dispose();
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

                if (_isPause)
                {
                    _statisticService.PauseTimer();
                }
                else
                {
                    _statisticService.ResumeTimer();
                }
            }

            if (_pauseCanvasView.IsEnabled != _playerInput.IsPause)
            {
                _playerInput.IsPause = _pauseCanvasView.IsEnabled;
                _isPause = _playerInput.IsPause;
            }
        }
        
        private void OpenSettingsWindow()
        {
            if (!_pauseCanvasView.IsEnabled) return;
            _settingsWindowCanvasView.ShowCanvas(true);
        }
    }
}
