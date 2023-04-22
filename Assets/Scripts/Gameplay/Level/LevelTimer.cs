using Gameplay.Mechanics.Timer;
using Scriptables;
using System;

namespace Gameplay.Level
{
    public sealed class LevelTimer : IDisposable
    {
        private readonly LevelFactory _levelFactory;

        public Timer Timer { get; }

        public event Action TimeIsOver = () => { };

        public LevelTimer(LevelConfig levelConfig, TimerFactory timerFactory, LevelFactory levelFactory)
        {
            Timer = timerFactory.Create(levelConfig.LevelTimer);
            _levelFactory = levelFactory;

            Timer.OnExpire += OnTimerExpire;
            _levelFactory.LevelCreated += OnLevelCreated;
        }

        public void Dispose()
        {
            Timer.OnExpire -= OnTimerExpire;
            Timer.Dispose();

            _levelFactory.LevelCreated -= OnLevelCreated;
        }

        public string GetTimerText()
        {
            var value = Timer.CurrentValue;

            if(Timer == null)
            {
                value = 0;
            }

            var timeSpan = TimeSpan.FromSeconds(value);
            return string.Format($"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}");
        }

        private void OnTimerExpire()
        {
            TimeIsOver.Invoke();
        }

        private void OnLevelCreated(Level obj)
        {
            Timer.Start();
        }
    }
}