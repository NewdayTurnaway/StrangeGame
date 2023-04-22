using Gameplay.Input;
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
        private readonly EndScreenCanvasView _endScreenCanvasView;
        private readonly StatisticService _statisticService;

        public EndScreenService(
            GameStateService gameStateService,
            CurrentGameState currentGameState,
            PlayerInput playerInput,
            StatisticServiceFactory statisticServiceFactory,
            EndScreenCanvasView endScreenCanvasView
            )
        {
            _gameStateService = gameStateService;
            _currentGameState = currentGameState;
            _playerInput = playerInput;
            _endScreenCanvasView = endScreenCanvasView;
            _statisticService = statisticServiceFactory.Create(_endScreenCanvasView.LevelStatsView);
        }

        public void Initialize()
        {
            _endScreenCanvasView.ShowCanvas(false);
            _endScreenCanvasView.Init(_currentGameState.StartNextLevel, _gameStateService.GoToMenu, _gameStateService.ExitGame);

            _currentGameState.NextLevelAction += OnNextLevelAction;
        }

        public void Dispose()
        {
            _currentGameState.NextLevelAction -= OnNextLevelAction;
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
