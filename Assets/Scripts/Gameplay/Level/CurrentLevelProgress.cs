using System;

namespace Gameplay.Level
{
    public sealed class CurrentLevelProgress : IDisposable
    {
        private readonly LevelTimer _levelTimer;

        public event Action<bool> LevelComplete = _ => { };

        public CurrentLevelProgress(LevelTimer levelTimer)
        {
            _levelTimer = levelTimer;

            _levelTimer.TimeIsOver += GameOver;
        }

        public void Dispose()
        {
            _levelTimer.TimeIsOver -= GameOver;
        }

        private void GameOver()
        {
            LevelComplete.Invoke(false);
        }
    }
}