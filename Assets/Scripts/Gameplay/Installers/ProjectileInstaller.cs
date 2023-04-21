using Gameplay.Projectile;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class ProjectileInstaller : MonoInstaller
    {
        [field: SerializeField] public Transform ProjectilePool { get; private set; }

        public override void InstallBindings()
        {
            InstallProjectileViewFactory();
            InstallProjectileFactory();
        }

        private void InstallProjectileViewFactory()
        {
            Container
                .Bind<Transform>()
                .FromInstance(ProjectilePool)
                .WhenInjectedInto<ProjectileViewFactory>();

            Container
                .BindFactory<ProjectileSpawnParams, ProjectileView, ProjectileViewFactory>()
                .AsSingle();
        }
        
        private void InstallProjectileFactory()
        {
            Container
                .BindFactory<ProjectileSpawnParams, Projectile.Projectile, ProjectileFactory>()
                .AsSingle();
        }
    }
}