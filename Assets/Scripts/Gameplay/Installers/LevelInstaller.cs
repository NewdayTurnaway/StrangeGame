using Gameplay.Level;
using Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [field: SerializeField] public Transform EnvironmentTransform { get; private set; }
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallLevelPart();
            InstallLevel();
            InstallLevelTimer();
            InstallLevelProgressService();
        }

        private void InstallLevelPart()
        {
            Container
                .Bind<Transform>()
                .FromInstance(EnvironmentTransform)
                .WhenInjectedInto<LevelPartFactory>();

            Container
                .BindFactory<Vector3, LevelPartView, LevelPart, LevelPartFactory>()
                .AsSingle();
        }
        
        private void InstallLevel()
        {
            Container
                .Bind<LevelConfig>()
                .FromInstance(LevelConfig)
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<int, Level.Level, LevelFactory>()
                .AsSingle();
        }
        
        private void InstallLevelTimer()
        {
            Container
                .BindInterfacesAndSelfTo<LevelTimer>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallLevelProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<CurrentLevelProgress>()
                .AsSingle()
                .NonLazy();
        }
    }
}