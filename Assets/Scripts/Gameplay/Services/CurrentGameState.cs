using Gameplay.Level;
using System;

namespace Gameplay.Services
{
    public sealed class CurrentGameState : IDisposable
    {
        private readonly LevelFactory _levelFactory;
        
        private Level.Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public CurrentGameState(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
            
            CurrentLevelNumber = 1;
            _currentLevel = levelFactory.Create(CurrentLevelNumber);
        }

        private void StartNextLevel()
        {
            _currentLevel.Dispose();
            CurrentLevelNumber += 1;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }

        public void Dispose()
        {
            _currentLevel.Dispose();
        }
    }
}