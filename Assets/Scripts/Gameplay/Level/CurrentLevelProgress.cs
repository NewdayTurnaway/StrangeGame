using Gameplay.Enemy;
using Gameplay.Player;
using Gameplay.Services;
using System;

namespace Gameplay.Level
{
    public sealed class CurrentLevelProgress : IDisposable
    {
        private readonly CurrentGameState _currentGameState;
        private readonly LevelTimer _levelTimer;
        private readonly EnemyCounter _enemyCounter;
        private readonly PlayerFactory _playerFactory;

        private Player.Player _player;

        public event Action<bool> LevelComplete = _ => { };

        public CurrentLevelProgress(CurrentGameState currentGameState, LevelTimer levelTimer, EnemyCounter enemyCounter, PlayerFactory playerFactory)
        {
            _currentGameState = currentGameState;
            _levelTimer = levelTimer;
            _enemyCounter = enemyCounter;
            _playerFactory = playerFactory;

            _playerFactory.PlayerCreated += OnPlayerCreated;
            _enemyCounter.CounterChanged += OnCounterChanged;
            _levelTimer.TimeIsOver += GameOver;
        }

        public void Dispose()
        {
            if (_player != null)
            {
                _player.PlayerFell -= PenaltyScore;
                _player.PlayerDestroyed -= GameOver;
            }

            _playerFactory.PlayerCreated -= OnPlayerCreated;
            _enemyCounter.CounterChanged -= OnCounterChanged;
            _levelTimer.TimeIsOver -= GameOver;
        }

        private void OnPlayerCreated(Player.Player player)
        {
            _player = player;
            _player.PlayerFell += PenaltyScore;
            _player.PlayerDestroyed += GameOver;
        }

        private void OnCounterChanged()
        {
            if (_enemyCounter.TotalCreatedEnemies == _enemyCounter.TotalDestroyedEnemies)
            {
                LevelComplete.Invoke(true);
            }
        }

        private void GameOver()
        {
            PenaltyScore();
            LevelComplete.Invoke(false);
        }

        private void PenaltyScore()
        {
            _currentGameState.UpdateScore(-25);
        }
    }
}