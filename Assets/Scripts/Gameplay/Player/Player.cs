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
        private readonly UnitAbilities _unitAbilities;

        public PlayerView PlayerView { get; private set; }

        public event Action PlayerDestroyed = () => { };
        public event Action PlayerFell = () => { };

        public Player(
            Updater updater,
            PlayerView playerView,
            PlayerMovement playerMovement,
            UnitAbilities unitAbilities
            )
        {
            _updater = updater;
            PlayerView = playerView;
            _playerMovement = playerMovement;
            _unitAbilities = unitAbilities;

            _updater.SubscribeToFixedUpdate(CheckHeight);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromFixedUpdate(CheckHeight);

            PlayerDestroyed.Invoke();

            _playerMovement.Dispose();
            _unitAbilities.Dispose();

            UnityEngine.Object.Destroy(PlayerView.gameObject);
        }

        private void CheckHeight()
        {
            if(PlayerView.transform.position.y <= LOWER_HEIGHT_LIMIT)
            {
                //TODO Penalty score
                PlayerFell.Invoke();
            }
        }

        private void OnDeath()
        {
            //TODO Penalty score
            Dispose();
        }
    }
}