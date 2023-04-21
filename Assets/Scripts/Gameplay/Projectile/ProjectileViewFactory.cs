using Gameplay.Projectile;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectile
{
    public sealed class ProjectileViewFactory : PlaceholderFactory<ProjectileSpawnParams, ProjectileView>
    {
        private readonly DiContainer _diContainer;
        private readonly Transform _projectilePool;

        public ProjectileViewFactory(DiContainer diContainer, Transform projectilePool)
        {
            _diContainer = diContainer;
            _projectilePool = projectilePool;
        }

        public override ProjectileView Create(ProjectileSpawnParams spawnParams)
        {
            var view = _diContainer.InstantiatePrefabForComponent<ProjectileView>(spawnParams.ProjectileView, spawnParams.Position, spawnParams.Rotation, _projectilePool);
            view.Rigidbody.AddForce(spawnParams.ForceToAdd, ForceMode.Impulse);
            return view;
        }
    }
}