using Gameplay.Player;
using System;
using System.Collections.Generic;

namespace Gameplay.Level
{
    public sealed class Level : IDisposable
    {
        private readonly List<LevelPart> _levelParts;

        private readonly Player.Player _player;
        //private readonly Enemies _enemies;

        public int CurrentLevelNumber { get; private set; }
        public LevelPart CurrentLevelPart { get; private set; }

        public Level(
            int currentLevelNumber,
            List<LevelPart> levelParts,
            PlayerFactory playerFactory
            )
        {
            CurrentLevelNumber = currentLevelNumber;
            _levelParts = levelParts;

            CurrentLevelPart = _levelParts[0];
            _player = playerFactory.Create(CurrentLevelPart.LevelPartView.PlayerSpawnPoint.position);
            //_enemies = enemies;

            foreach (var levelPart in _levelParts)
            {
                levelPart.LevelPartChanged += OnLevelPartChanged;
            }
        }

        public void Dispose()
        {
            foreach (var levelPart in _levelParts)
            {
                levelPart.LevelPartChanged -= OnLevelPartChanged;
                levelPart.Dispose();
            }
            _levelParts.Clear();

            _player.Dispose();
            //_enemies.Dispose();
        }

        private void OnLevelPartChanged(LevelPart levelPart)
        {
            CurrentLevelPart = levelPart;
        }
    }
}