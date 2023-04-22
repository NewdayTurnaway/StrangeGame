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

        public Action<string> LevelNumberReceived = _ => { };
        public Action<string> TimerTextChanged = _ => { };

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

        public void PauseTimer()
        {
            _levelTimer.Timer.Pause();
        }
        
        public void ResumeTimer()
        {
            _levelTimer.Timer.Resume();
        }

        private void UpdateTimerText()
        {
            var text = _levelTimer.GetTimerText();
            _levelProgressInfoView.Timer.text = text;
            TimerTextChanged.Invoke(text);
        }

        private void OnLevelCreated(Level level)
        {
            _level = level;

            var levelNumber = _level.CurrentLevelNumber.ToString();
            _levelProgressInfoView.LevelNumber.text = levelNumber;
            LevelNumberReceived.Invoke(levelNumber);
            _levelProgressInfoView.ShowCanvas(true);

            _updater.SubscribeToUpdate(UpdateTimerText);
        }
    }
}
