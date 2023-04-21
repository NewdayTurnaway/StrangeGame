using Gameplay.Unit;
using UnityEngine;

namespace Gameplay.Projectile
{
    public sealed class ProjectileSpawnParams
    {
        public ProjectileView ProjectileView { get; private set; }

        public Vector3 Position => _spawnPoint.position;
        public Quaternion Rotation => _cameraTransform.rotation;

        public ProjectileInfo ProjectileInfo { get; private set; }

        public Vector3 ForceToAdd { get; set; }

        private Transform _spawnPoint;
        private Transform _cameraTransform;

        public ProjectileSpawnParams(
            ProjectileView projectileView,
            Transform spawnPoint,
            Transform cameraTransform,
            ProjectileInfo projectileInfo,
            Vector3 forceToAdd = default)
        {
            ProjectileView = projectileView;
            _spawnPoint = spawnPoint;
            _cameraTransform = cameraTransform;
            ProjectileInfo = projectileInfo;
            ForceToAdd = forceToAdd;
        }
    }
}