using Gameplay.Level;
using Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }

        public override void InstallBindings()
        {
            InstallLevelPart();
            InstallLevel();
            //InstallLevelProgressService();
        }

        private void InstallLevelPart()
        {
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

        //private void InstallLevelProgressService()
        //{
        //    Container
        //        .BindInterfacesAndSelfTo<CurrentLevelProgress>()
        //        .AsSingle()
        //        .NonLazy();
        //}
    }
}