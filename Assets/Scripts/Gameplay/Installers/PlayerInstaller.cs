using Gameplay.Player;
using Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallPlayer();
        }

        private void InstallPlayer()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(PlayerConfig)
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<Vector3, Player.Player, PlayerFactory>()
                .AsSingle();
        }
    }
}