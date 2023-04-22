using Gameplay.Enemy;
using Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallEnemy();
            InstallEnemyCounter();
        }

        private void InstallEnemy()
        {
            Container
                .Bind<EnemyConfig>()
                .FromInstance(EnemyConfig)
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<Vector3, Enemy.Enemy, EnemyFactory>()
                .AsSingle();
        }
        
        private void InstallEnemyCounter()
        {
            Container
                .Bind<EnemyCounter>()
                .AsSingle()
                .NonLazy();
        }
    }
}