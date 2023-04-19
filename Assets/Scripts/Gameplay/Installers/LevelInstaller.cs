using Gameplay.Level;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallLevel();
            //InstallLevelProgressService();
        }

        private void InstallLevel()
        {
            //LevelConfig

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