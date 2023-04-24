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

            _currentGameState.NextLevelAction += OnNextLevelAction;
            _enemyFactory.EnemyCreated += OnEnemyCreated;
        }

        public void Dispose()
        {
            _currentGameState.NextLevelAction -= OnNextLevelAction;
            _enemyFactory.EnemyCreated -= OnEnemyCreated;
        }

        private void OnNextLevelAction()
        {
            TotalCreatedEnemies = 0;
            TotalDestroyedEnemies = 0;
        }

        private void OnEnemyCreated(Enemy enemy)
        {
            enemy.CountedMurder += OnEnemyDestroyed;
            TotalCreatedEnemies++;
            CounterChanged.Invoke();
        }

        private void OnEnemyDestroyed(Enemy enemy)
        {
            enemy.CountedMurder -= OnEnemyDestroyed;
            TotalDestroyedEnemies++;
            CounterChanged.Invoke();
            _currentGameState.UpdateScore(10);
        }
    }
}