using Gameplay.Services;
using System;

namespace Gameplay.Enemy
{
    public sealed class EnemyCounter : IDisposable
    {
        private readonly CurrentGameState _currentGameState;
        private readonly EnemyFactory _enemyFactory;

        public int TotalCreatedEnemies { get; private set; }
        public int TotalDestroyedEnemies { get; private set; }

        public event Action CounterChanged = () => { };

        public EnemyCounter(CurrentGameState currentGameState, EnemyFactory enemyFactory)
        {
            _currentGameState = currentGameState;
            _enemyFactory = enemyFactory;

            _enemyFactory.EnemyCreated += OnEnemyCreated;
        }

        public void Dispose()
        {
            _enemyFactory.EnemyCreated -= OnEnemyCreated;
        }

        private void OnEnemyCreated(Enemy enemy)
        {
            enemy.EnemyDestroyed += OnEnemyDestroyed;
            TotalCreatedEnemies++;
            CounterChanged.Invoke();
        }

        private void OnEnemyDestroyed(Enemy enemy)
        {
            enemy.EnemyDestroyed -= OnEnemyDestroyed;
            TotalDestroyedEnemies++;
            CounterChanged.Invoke();
            _currentGameState.UpdateScore(10);
        }
    }
}