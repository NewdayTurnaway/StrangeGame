using Gameplay.Level;
using Services;
using System;

namespace UI.Services
{
    public sealed class LevelProgressInfoService : IDisposable
    {
        private readonly Updater _updater;
        private readonly LevelProgressInfoView _levelProgressInfoView;
        private readonly LevelTimer _levelTimer;
        private readonly LevelFactory _levelFactory;

        private Level _level;

        public LevelProgressInfoService(Updater updater, LevelProgressInfoView levelProgressInfoView, LevelTimer levelTimer, LevelFactory levelFactory)
        {
            _updater = updater;
            _levelProgressInfoView = levelProgressInfoView;
            _levelTimer = levelTimer;
            _levelFactory = levelFactory;
         
            _levelProgressInfoView.ShowCanvas(false);
            _levelFactory.LevelCreated += OnLevelCreated;
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(UpdateTimerText);
            _levelFactory.LevelCreated -= OnLevelCreated;
        }

        private void UpdateTimerText()
        {
            _levelProgressInfoView.Timer.text = GetTimerText(_levelTimer.Timer.CurrentValue);
        }

        private void OnLevelCreated(Level level)
        {
            _level = level;

            _levelProgressInfoView.LevelNumber.text = _level.CurrentLevelNumber.ToString();
            _levelProgressInfoView.ShowCanvas(true);

            _updater.SubscribeToUpdate(UpdateTimerText);
        }

        private string GetTimerText(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return string.Format($"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}");
        }
    }
}
