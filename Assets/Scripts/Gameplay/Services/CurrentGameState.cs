using Gameplay.Level;
using Services;
using System;
using Zenject;

namespace Gameplay.Services
{
    public sealed class CurrentGameState : IInitializable,  IDisposable
    {
        private readonly PlayerDataService _playerDataService;
        private readonly LevelFactory _levelFactory;
        
        private Level.Level _currentLevel;
        
        public int CurrentLevelNumber { get; private set; }
        public int CurrentScore { get; private set; }
        public int RecordScore { get; private set; }

        public event Action NextLevelAction = () => { };
        public event Action ScoreChanged = () => { };

        public CurrentGameState(PlayerDataService playerDataService, LevelFactory levelFactory)
        {
            _playerDataService = playerDataService;
            _levelFactory = levelFactory;
        }

        public void Initialize()
        {
            RecordScore = _playerDataService.RecordScore;
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

        public void UpdateScore(int additionalScore)
        {
            var newScore = CurrentScore + additionalScore;
            if(newScore < 0)
            {
                newScore = 0;
            }
            CurrentScore = newScore;

            if (RecordScore < CurrentScore)
            {
                RecordScore = CurrentScore;
                _playerDataService.UpdateRecordScore(RecordScore);
            }
            ScoreChanged.Invoke();
        }

        public void Dispose()
        {
            _currentLevel.Dispose();
        }
    }
}