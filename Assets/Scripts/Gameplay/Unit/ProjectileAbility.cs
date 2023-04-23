using Gameplay.Enemy;
using Gameplay.Mechanics.Timer;
using Gameplay.Projectile;
using Scriptables;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Unit
{
    public sealed class ProjectileAbility : IDisposable
    {
        private const float RAYCAST_MAX_DISTANCE = 500f;

        private readonly Transform _unitTransform;
        private readonly Transform _headTransform;
        private readonly ProjectileFactory _projectileFactory;

        private readonly Transform _throwPoint;
        private readonly ProjectileConfig _projectileConfig;
        private readonly Timer _timer;

        private readonly ProjectileSpawnParams _projectileSpawnParams;

        private int _currentTotalThrows;
        private bool _readyToThrow = true;
        private bool _notAccurateAim;

        public int CurrentTotalThrows => _currentTotalThrows;

        public event Action TotalThrowsChanged = () => { };
        public event Action AbilityAvailable = () => { };

        public ProjectileAbility(
            ProjectileFactory projectileFactory,
            UnitView unitView,
            ProjectileConfig projectileConfig,
            Timer timer)
        {
            _unitTransform = unitView.transform;
            _headTransform = unitView.Head.transform;
            _projectileFactory = projectileFactory;
            _throwPoint = unitView.ThrowPoint;
            _projectileConfig = projectileConfig;
            _timer = timer;

            if(unitView is EnemyView)
            {
                _notAccurateAim = true;
            }
            
            _timer.OnExpire += ResetThrow;

            var projectileInfo = new ProjectileInfo(
                _projectileConfig.ProjectileType,
                unitView.UnitType,
                _projectileConfig.Damage,
                _projectileConfig.LifeTime,
                _projectileConfig.IsDestroyedOnHit,
                _projectileConfig.ExplosionEffect,
                _projectileConfig.ExplosionRadius,
                _projectileConfig.ExplosionForce);

            _projectileSpawnParams = new(
                _projectileConfig.ProjectileView,
                _throwPoint,
                _headTransform,
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
            var forceDirection = _headTransform.forward;

            if(Physics.Raycast(_headTransform.position, _headTransform.forward, out var hit, RAYCAST_MAX_DISTANCE))
            {
                forceDirection = (hit.point - _throwPoint.position).normalized;
            }

            if (_notAccurateAim)
            {
                forceDirection.x += Random.Range(0, 0.2f);
                forceDirection.y += Random.Range(0, 0.1f);
            }
            
            var forceToAdd = forceDirection * _projectileConfig.ThrowForce + _unitTransform.up * _projectileConfig.ThrowUpwardForce;


            _projectileSpawnParams.ForceToAdd = forceToAdd;
            _projectileFactory.Create(_projectileSpawnParams);
            _currentTotalThrows--;
            TotalThrowsChanged.Invoke();
        }

        private void ResetThrow()
        {
            _readyToThrow = true;
            AbilityAvailable.Invoke();
        }
    }
}