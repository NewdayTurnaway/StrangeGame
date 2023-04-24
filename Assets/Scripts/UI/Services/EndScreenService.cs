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

        private bool _isComplete;

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
            _endScreenCanvasView.Init(_currentGameState.StartNextLevel, GoToMenu, ExitGame);

            _currentGameState.NextLevelAction += OnNextLevelAction;
            _currentLevelProgress.LevelComplete += OpenEndScreenWindow;
        }

        private void ExitGame()
        {
            if (_isComplete)
            {
                _currentGameState.UpdateRecordScore();
            }
            _isComplete = false;
            _gameStateService.ExitGame();
        }

        private void GoToMenu()
        {
            if (_isComplete)
            {
                _currentGameState.UpdateRecordScore();
            }
            _isComplete = false;
            _gameStateService.GoToMenu();
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
            _statisticService.ResumeTimer();
            _endScreenCanvasView.ShowCanvas(false);
        }

        private void OpenEndScreenWindow(bool isLevelComplete)
        {
            _isComplete = isLevelComplete;
            _playerInput.SetActivePauseButton(false);
            _playerInput.IsPause = true;
            _statisticService.PauseTimer();

            if (_isComplete)
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
