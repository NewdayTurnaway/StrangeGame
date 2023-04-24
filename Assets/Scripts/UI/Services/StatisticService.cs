using Gameplay.Enemy;
using Gameplay.Services;
using System;
using Zenject;

namespace UI.Services
{
    public sealed class StatisticService : IDisposable
    {
        private readonly CurrentGameState _currentGameState;
        private readonly EnemyCounter _enemyCounter;
        private readonly LevelProgressInfoService _levelProgressInfoService;
        private readonly LevelStatsView _levelStatsView;

        public StatisticService(
            CurrentGameState currentGameState,
            EnemyCounter enemyCounter,
            LevelProgressInfoService levelProgressInfoService,
            LevelStatsView levelStatsView
            )
        {
            _currentGameState = currentGameState;
            _enemyCounter = enemyCounter;
            _levelProgressInfoService = levelProgressInfoService;
            _levelStatsView = levelStatsView;

            _currentGameState.ScoreChanged += OnScoreChanged;
            _enemyCounter.CounterChanged += OnCounterChanged;
            _levelProgressInfoService.LevelNumberReceived += OnLevelNumberReceived;
            _levelProgressInfoService.TimerTextChanged += OnTimerTextChanged;

            OnScoreChanged();
        }

        public void Dispose()
        {
            _currentGameState.ScoreChanged -= OnScoreChanged;
            _enemyCounter.CounterChanged -= OnCounterChanged;
            _levelProgressInfoService.LevelNumberReceived -= OnLevelNumberReceived;
            _levelProgressInfoService.TimerTextChanged -= OnTimerTextChanged;
        }

        public void PauseTimer()
        {
            _levelProgressInfoService.PauseTimer();
        }

        public void ResumeTimer()
        {
            _levelProgressInfoService.ResumeTimer();
        }

        private void OnScoreChanged()
        {
            _levelStatsView.Score.text = _currentGameState.CurrentScore.ToString();
            _levelStatsView.RecordScore.text = _currentGameState.RecordScore.ToString();
        }
        
        private void OnCounterChanged()
        {
            _levelStatsView.EnemiesCount.text = $"{_enemyCounter.TotalDestroyedEnemies} / {_enemyCounter.TotalCreatedEnemies}";
        }

        private void OnLevelNumberReceived(string levelNumberText)
        {
            _levelStatsView.LevelNumber.text = levelNumberText;
        }

        private void OnTimerTextChanged(string timerText)
        {
            _levelStatsView.Timer.text = timerText;
        }
    }
}
