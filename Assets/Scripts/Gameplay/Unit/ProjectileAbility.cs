using Gameplay.Mechanics.Timer;
using Gameplay.Projectile;
using Scriptables;
using System;
using UnityEngine;

namespace Gameplay.Unit
{
    public sealed class ProjectileAbility : IDisposable
    {
        private const float RAYCAST_MAX_DISTANCE = 500f;

        private readonly Camera _mainCamera;
        private readonly ProjectileFactory _projectileFactory;

        private readonly Transform _unitTransform;
        private readonly Transform _throwPoint;
        private readonly ProjectileConfig _projectileConfig;
        private readonly Timer _timer;

        private readonly ProjectileSpawnParams _projectileSpawnParams;

        private int _currentTotalThrows;
        private bool _readyToThrow = true;

        public ProjectileAbility(
            Camera mainCamera,
            ProjectileFactory projectileFactory,
            UnitView unitView,
            ProjectileConfig projectileConfig,
            Timer timer)
        {
            _mainCamera = mainCamera;
            _projectileFactory = projectileFactory;
            _unitTransform = unitView.transform;
            _throwPoint = unitView.ThrowPoint;
            _projectileConfig = projectileConfig;
            _timer = timer;
            
            _timer.OnExpire += ResetThrow;

            var projectileInfo = new ProjectileInfo(
                _projectileConfig.ProjectileType,
                unitView.UnitType,
                _projectileConfig.Damage,
                _projectileConfig.LifeTime,
                _projectileConfig.IsDestroyedOnHit,
                _projectileConfig.ExplosionRadius,
                _projectileConfig.ExplosionForce);

            _projectileSpawnParams = new(
                _projectileConfig.ProjectileView,
                _throwPoint,
                _mainCamera.transform,
                projectileInfo
                );

            _currentTotalThrows = _projectileConfig.TotalThrows;
        }

        public void Dispose()
        {
            _timer.OnExpire -= ResetThrow;
            _timer.Dispose();
        }

        public void ApplyAbility(bool abilityInput)
        {
            if (abilityInput && _readyToThrow && _currentTotalThrows > 0)
            {
                _readyToThrow = false;
                Throw();
                _timer.Start();
            }
        }

        private void Throw()
        {
            var forceDirection = _mainCamera.transform.forward;

            if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out var hit, RAYCAST_MAX_DISTANCE))
            {
                forceDirection = (hit.point - _throwPoint.position).normalized;
            }

            var forceToAdd = forceDirection * _projectileConfig.ThrowForce + _throwPoint.up * _projectileConfig.ThrowUpwardForce;
            _projectileSpawnParams.ForceToAdd = forceToAdd;
            _projectileFactory.Create(_projectileSpawnParams);
            _currentTotalThrows--;
        }

        private void ResetThrow()
        {
            _readyToThrow = true;
        }
    }
}