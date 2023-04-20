using Gameplay.Level.Generator;
using Scriptables;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Gameplay.Level
{
    public sealed class LevelFactory : PlaceholderFactory<int, Level>
    {
        private readonly LevelConfig _levelConfig;
        private readonly LevelPartFactory _levelPartFactory;

        private readonly LevelGenerator _levelGenerator;

        public event Action<Level> LevelCreated = (_) => { };

        public LevelFactory(LevelConfig levelConfig, LevelPartFactory levelPartFactory)
        {
            _levelConfig = levelConfig;
            _levelPartFactory = levelPartFactory;

            _levelGenerator = new LevelGenerator();
        }

        public override Level Create(int levelNumber)
        {
            var levelParts = new List<LevelPart>();
            var coordinates = _levelGenerator.GenerateCoordinates(_levelConfig.LevelPartCount);

            for (int i = 0; i < _levelConfig.LevelPartCount; i++)
            {
                var coordinate = new Vector3(coordinates[i].x * _levelConfig.LevelPartSize, 0, coordinates[i].y * _levelConfig.LevelPartSize);
                var levelPart = _levelPartFactory.Create(coordinate, PickRandomLevelPartView());
                levelParts.Add(levelPart);
            }

            var level = new Level(levelNumber, levelParts);
            LevelCreated.Invoke(level);
            return level;
        }

        private LevelPartView PickRandomLevelPartView()
        {
            var index = new Random().Next(_levelConfig.LevelPartViews.Count);
            return _levelConfig.LevelPartViews[index];
        }
    }
}