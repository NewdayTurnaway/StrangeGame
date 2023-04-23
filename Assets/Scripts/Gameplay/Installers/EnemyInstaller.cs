using Gameplay.Enemy;
using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Player;
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
            InstallEnemyInput();
            InstallEnemyMovement();
            InstallEnemyBehaviour();
            InstallEnemy();
            InstallEnemyCounter();
        }

        private void InstallEnemyInput()
        {
            Container
                 .BindFactory<EnemyInput, EnemyInputFactory>()
                 .AsSingle();
        }

        private void InstallEnemyMovement()
        {
            Container
                 .BindFactory<EnemyView, EnemyInput, EnemyMovement, EnemyMovementFactory>()
                 .AsSingle();
        }
        
        private void InstallEnemyBehaviour()
        {
            Container
                 .BindFactory<EnemyView, EnemyInput, LevelPartView, EnemyConfig, EnemyPassiveRoamingBehaviour, EnemyPassiveRoamingBehaviourFactory>()
                 .AsSingle();
            
            Container
                 .BindFactory<EnemyView, EnemyInput, LevelPartView, EnemyConfig, PlayerView, EnemyInCombatBehaviour, EnemyInCombatBehaviourFactory>()
                 .AsSingle();
            
            Container
                 .BindFactory<EnemyView, EnemyInput, LevelPartView, EnemyBehaviourSwitcher, EnemyBehaviourSwitcherFactory>()
                 .AsSingle();
        }

        private void InstallEnemy()
        {
            Container
                .Bind<EnemyConfig>()
                .FromInstance(EnemyConfig)
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<Vector3, LevelPartView, Enemy.Enemy, EnemyFactory>()
                .AsSingle();
        }
        
        private void InstallEnemyCounter()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyCounter>()
                .AsSingle()
                .NonLazy();
        }
    }
}