using Services;
using System;

namespace UI.Services
{
    public sealed class StatisticService : IDisposable
    {
        private readonly PlayerDataService _playerDataService;
        private readonly LevelProgressInfoService _levelProgressInfoService;
        private readonly LevelStatsView _levelStatsView;

        public StatisticService(
            PlayerDataService playerDataService,
            LevelProgressInfoService levelProgressInfoService,
            LevelStatsView levelStatsView
            )
        {
            _playerDataService = playerDataService;
            _levelProgressInfoService = levelProgressInfoService;
            _levelStatsView = levelStatsView;

            _levelProgressInfoService.LevelNumberReceived += OnLevelNumberReceived;
            _levelProgressInfoService.TimerTextChanged += OnTimerTextChanged;
        }

        public void Dispose()
        {
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
