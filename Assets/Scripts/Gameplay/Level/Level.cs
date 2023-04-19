using System;

namespace Gameplay.Level
{
    public sealed class Level : IDisposable
    {
        //private readonly Player _player;
        //private readonly Enemies _enemies;
        //private readonly LevelEnvironment _levelEnvironment;

        public int CurrentLevelNumber { get; private set; }

        public Level(
            int currentLevelNumber
            )
        {
            CurrentLevelNumber = currentLevelNumber;
            //_player = player;
            //_enemies = enemies;
            //_levelEnvironment = levelEnvironment;
        }

        public void Dispose()
        {
            //_player.Dispose();
            //_enemies.Dispose();
            //_levelEnvironment.Dispose();
        }
    }
}