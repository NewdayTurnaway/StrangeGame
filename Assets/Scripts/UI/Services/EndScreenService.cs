using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Services;
using Services;
using System;
using Zenject;

namespace UI.Services
{
    public sealed class EndScreenService : IInitializable, IDisposable
    {
        private readonly GameStateService _gameStateService;
        private readonly CurrentGameState _currentGameState;
        private readonly PlayerInput _playerInput;
        private readonly CurrentLevelProgress _currentLevelProgress;
        private readonly EndScreenCanvasView _endScreenCanvasView;
        private readonly StatisticService _statisticService;

        public EndScreenService(
            GameStateService gameStateService,
            CurrentGameState currentGameState,
            PlayerInput playerInput,
            CurrentLevelProgress currentLevelProgress,
            StatisticServiceFactory statisticServiceFactory,
            EndScreenCanvasView endScreenCanvasView
            )
        {
            _gameStateService = gameStateService;
            _currentGameState = currentGameState;
            _playerInput = playerInput;
            _currentLevelProgress = currentLevelProgress;
            _endScreenCanvasView = endScreenCanvasView;
            _statisticService = statisticServiceFactory.Create(_endScreenCanvasView.LevelStatsView);
        }

        public void Initialize()
        {
            _endScreenCanvasView.ShowCanvas(false);
            _endScreenCanvasView.Init(_currentGameState.StartNextLevel, _gameStateService.GoToMenu, _gameStateService.ExitGame);

            _currentGameState.NextLevelAction += OnNextLevelAction;
            _currentLevelProgress.LevelComplete += OpenEndScreenWindow;
        }

        public void Dispose()
        {
            _currentGameState.NextLevelAction -= OnNextLevelAction;
            _currentLevelProgress.LevelComplete -= OpenEndScreenWindow;
            _statisticService.Dispose();
        }

        private void OnNextLevelAction()
        {
            _playerInput.SetActivePauseButton(true);
            _playerInput.IsPause = false;
            _endScreenCanvasView.ShowCanvas(false);
        }

        private void OpenEndScreenWindow(bool isLevelComplete)
        {
            _playerInput.SetActivePauseButton(false);
            _playerInput.IsPause = true;
            _statisticService.PauseTimer();

            if (isLevelComplete)
            {
                _endScreenCanvasView.ShowLevelComplete(); 
            }
            else
            {
                _endScreenCanvasView.ShowGameOver();
            }

            _endScreenCanvasView.ShowCanvas(true);
        }
    }
}
