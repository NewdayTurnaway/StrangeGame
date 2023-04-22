using System;
using System.Collections.Generic;

namespace Gameplay.Level
{
    public sealed class LevelPart : IDisposable
    {
        public LevelPartView LevelPartView { get; }
        public List<Enemy.Enemy> Enemies  { get; }

        public event Action<LevelPart> LevelPartChanged = _ => { };

        public LevelPart(LevelPartView levelPartView, List<Enemy.Enemy> enemies)
        {
            LevelPartView = levelPartView;
            Enemies = enemies;

            foreach (var enemy in Enemies)
            {
                enemy.EnemyDestroyed += OnEnemyDestroyed;
            }

            LevelPartView.PlayerInThisLevelPart += PlayerChangeLevelPart;
        }

        public void Dispose()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Dispose();
            }

            Enemies.Clear();

            LevelPartView.PlayerInThisLevelPart -= PlayerChangeLevelPart;
        }

        private void OnEnemyDestroyed(Enemy.Enemy enemy)
        {
            enemy.EnemyDestroyed -= OnEnemyDestroyed;
            Enemies.Remove(enemy);
        }

        private void PlayerChangeLevelPart()
        {
            LevelPartChanged.Invoke(this);
        }
    }
}