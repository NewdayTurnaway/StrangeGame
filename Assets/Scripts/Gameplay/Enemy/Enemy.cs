using Gameplay.Unit;
using Services;
using System;

namespace Gameplay.Enemy
{
    public sealed class Enemy : IDisposable
    {
        private const int LOWER_HEIGHT_LIMIT = -100;

        private readonly Updater _updater;
        private readonly EnemyMovement _enemyMovement;
        private readonly UnitHealth _unitHealth;
        private readonly UnitAbilities _unitAbilities;

        public EnemyView EnemyView { get; }
        public UnitHealth UnitHealth => _unitHealth;

        public event Action<Enemy> EnemyDestroyed = _ => { };

        public Enemy(
            Updater updater,
            EnemyView enemyView,
            EnemyMovement enemyMovement,
            UnitHealth unitHealth,
            UnitAbilities unitAbilities
            )
        {
            _updater = updater;
            EnemyView = enemyView;
            _enemyMovement = enemyMovement;
            _unitHealth = unitHealth;
            _unitAbilities = unitAbilities;

            _unitHealth.HealthReachedZero += OnDeath;
            _updater.SubscribeToFixedUpdate(CheckHeight);
        }

        public void Dispose()
        {
            _unitHealth.HealthReachedZero -= OnDeath;
            _updater.UnsubscribeFromFixedUpdate(CheckHeight);

            EnemyDestroyed.Invoke(this);

            _enemyMovement.Dispose();
            _unitAbilities.Dispose();

            if(EnemyView != null) UnityEngine.Object.Destroy(EnemyView.gameObject);
        }

        private void CheckHeight()
        {
            if (EnemyView == null) return;
            if (EnemyView.transform.position.y <= LOWER_HEIGHT_LIMIT)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            Dispose();
        }
    }
}