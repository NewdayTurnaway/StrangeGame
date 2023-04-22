using Gameplay.Level;
using System;
using Zenject;

namespace Gameplay.Services
{
    public sealed class CurrentGameState : IInitializable,  IDisposable
    {
        private readonly LevelFactory _levelFactory;
        
        private Level.Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }

        public event Action NextLevelAction = () => { };

        public CurrentGameState(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
        }

        public void Initialize()
        {
            CurrentLevelNumber = 1;
            _currentLevel = _levelFactory.Create(CurrentLevelNumber);
        }


        public void StartNextLevel()
        {
            NextLevelAction.Invoke();
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