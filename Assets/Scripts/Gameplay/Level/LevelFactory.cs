using System;
using Zenject;

namespace Gameplay.Level
{
    public sealed class LevelFactory : PlaceholderFactory<int, Level>
    {
        //private readonly LevelConfig

        public event Action<Level> LevelCreated = (_) => { };

        public override Level Create(int levelNumber)
        {
            var level = new Level(levelNumber);
            LevelCreated.Invoke(level);
            return level;
        }
    }
}