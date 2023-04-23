using Gameplay.Enemy;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.Level
{
    public sealed class LevelPartFactory : PlaceholderFactory<Vector3, LevelPartView, LevelPart>
    {
        private readonly DiContainer _diContainer;
        private readonly Transform _environmentTransform;
        private readonly EnemyFactory _enemyFactory;

        public LevelPartFactory(DiContainer diContainer, Transform environmentTransform, EnemyFactory enemyFactory)
        {
            _diContainer = diContainer;
            _environmentTransform = environmentTransform;
            _enemyFactory = enemyFactory;
        }

        public override LevelPart Create(Vector3 position, LevelPartView levelPartView)
        {
            var view = _diContainer.InstantiatePrefabForComponent<LevelPartView>(levelPartView, _environmentTransform);
            view.transform.position = position;

            var enemies = new List<Enemy.Enemy>();
            foreach (var spawnPoint in view.EnemySpawnPoints)
            {
                var enemy = _enemyFactory.Create(spawnPoint.position, view);
                enemies.Add(enemy);
            }

            return new(view, enemies);
        }
    }
}