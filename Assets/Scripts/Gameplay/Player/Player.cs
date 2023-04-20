using System;

namespace Gameplay.Player
{
    public sealed class Player : IDisposable
    {
        public PlayerView PlayerView { get; private set; }

        public event Action PlayerDestroyed = () => { };

        public Player(
            PlayerView playerView
            )
        {
            PlayerView = playerView;
        }

        public void Dispose()
        {
            PlayerDestroyed.Invoke();

            UnityEngine.Object.Destroy(PlayerView.gameObject);
        }

        private void OnDeath()
        {
            Dispose();
        }
    }
}