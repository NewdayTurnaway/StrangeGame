using System;

namespace Gameplay.Unit
{
    public sealed class UnitHealth : IDisposable
    {
        private readonly UnitView _unitView;

        public float StartHealth { get; }
        public float Health { get; private set; }

        public event Action HealthReachedZero = () => { };
        public event Action HealthChanged = () => { };

        public UnitHealth(UnitView unitView, float startHealth)
        {
            _unitView = unitView;
            StartHealth = startHealth;
            Health = StartHealth;

            _unitView.DamageTaken += TakeDamage;
        }

        public void Dispose()
        {
            _unitView.DamageTaken += TakeDamage;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;

            if(Health <= 0)
            {
                Health = 0;
                HealthChanged.Invoke();
                HealthReachedZero.Invoke();
            }
            else
            {
                HealthChanged.Invoke();
            }
        }
    }
}