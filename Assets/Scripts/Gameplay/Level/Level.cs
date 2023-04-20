using System;
using System.Collections.Generic;

namespace Gameplay.Level
{
    public sealed class Level : IDisposable
    {
        private readonly List<LevelPart> _levelParts;

        //private readonly Player _player;
        //private readonly Enemies _enemies;
        //private readonly LevelEnvironment _levelEnvironment;

        public int CurrentLevelNumber { get; private set; }

        public Level(
            int currentLevelNumber,
            List<LevelPart> levelParts
            )
        {
            CurrentLevelNumber = currentLevelNumber;
            _levelParts = levelParts;
            //_player = player;
            //_enemies = enemies;
            //_levelEnvironment = levelEnvironment;
        }

        public void Dispose()
        {
            foreach (var levelPart in _levelParts)
            {
                levelPart.Dispose();
            }
            _levelParts.Clear();

            //_player.Dispose();
            //_enemies.Dispose();
            //_levelEnvironment.Dispose();
        }
    }
}