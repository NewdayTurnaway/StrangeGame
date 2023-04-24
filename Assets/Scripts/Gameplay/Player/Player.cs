using Gameplay.Unit;
using Services;
using System;

namespace Gameplay.Player
{
    public sealed class Player : IDisposable
    {
        private const int LOWER_HEIGHT_LIMIT = -100;

        private readonly Updater _updater;
        private readonly PlayerMovement _playerMovement;
        private readonly UnitHealth _unitHealth;
        private readonly UnitAbilities _unitAbilities;

        public PlayerView PlayerView { get; }

        public UnitAbilities UnitAbilities => _unitAbilities;
        public UnitHealth UnitHealth => _unitHealth;

        public event Action PlayerDestroyed = () => { };
        public event Action PlayerMurdered = () => { };
        public event Action PlayerFell = () => { };

        public Player(
            Updater updater,
            PlayerView playerView,
            PlayerMovement playerMovement,
            UnitHealth unitHealth,
            UnitAbilities unitAbilities
            )
        {
            _updater = updater;
            PlayerView = playerView;
            _playerMovement = playerMovement;
            _unitHealth = unitHealth;
            _unitAbilities = unitAbilities;

            _unitHealth.HealthReachedZero += OnDeath;
            _updater.SubscribeToFixedUpdate(CheckHeight);
        }

        public void Dispose()
        {
            _unitHealth.HealthReachedZero -= OnDeath;
            _updater.UnsubscribeFromFixedUpdate(CheckHeight);

            PlayerDestroyed.Invoke();

            _playerMovement.Dispose();
            _unitAbilities.Dispose();

            if(PlayerView != null) UnityEngine.Object.Destroy(PlayerView.gameObject);
        }

        private void CheckHeight()
        {
            if (PlayerView == null) return;
            if (PlayerView.transform.position.y <= LOWER_HEIGHT_LIMIT)
            {
                PlayerFell.Invoke();
            }
        }

        private void OnDeath()
        {
            PlayerMurdered.Invoke();
            Dispose();
        }
    }
}