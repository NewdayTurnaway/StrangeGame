using Gameplay.Mechanics.Timer;
using Gameplay.Unit;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Projectile
{
    public sealed class Projectile : IDisposable
    {
        private readonly ProjectileView _projectileView;
        private readonly Timer _lifeTime;
        private readonly ProjectileInfo _projectileInfo;

        private bool _isTargetHit;

        public Projectile(ProjectileView projectileView, Timer timer, ProjectileInfo projectileInfo)
        {
            _projectileView = projectileView;

            _lifeTime = timer;
            _projectileInfo = projectileInfo;
            _lifeTime.OnExpire += Dispose;
            _projectileView.CollidedObject += OnCollided;
            _projectileView.DamagedUnit += OnDamage;

            _lifeTime.Start();
        }

        public void Dispose()
        {
            _lifeTime.OnExpire -= Dispose;
            _projectileView.CollidedObject -= OnCollided;
            _projectileView.DamagedUnit += OnDamage;
            _lifeTime.Dispose();

            if(_projectileInfo.ProjectileType == ProjectileType.Explosive)
            {
                Explosion();
                Object.Destroy(_projectileView.gameObject, 0.1f);
            }
            else
            {
                Object.Destroy(_projectileView.gameObject);
            }
        }

        private void OnCollided()
        {
            if (_isTargetHit)
            {
                return;
            }
            else
            {
                _isTargetHit = true;
            }

            if (_projectileInfo.IsDestroyedOnHit)
            {
                Dispose();
                return;
            }

            _projectileView.Rigidbody.isKinematic = true;
        }

        private void OnDamage(UnitView unitView)
        {
            if (_projectileInfo.ProjectileType != ProjectileType.Explosive && _projectileInfo.OwnerUnitType != unitView.UnitType)
            {
                unitView.TakeDamage(_projectileInfo.Damage);
            }

            Dispose();
        }

        private void Explosion()
        {
            //TODO ExplosionEffects

            var projectilePosition = _projectileView.transform.position;
            var objectsInRange = Physics.OverlapSphere(projectilePosition, _projectileInfo.ExplosionRadius);

            for (int i = 0; i < objectsInRange.Length; i++)
            {
                if (objectsInRange[i].TryGetComponent<UnitView>(out var unitView) && _projectileInfo.OwnerUnitType == unitView.UnitType)
                {
                    unitView.TakeDamage(_projectileInfo.Damage);
                }
                
                if (objectsInRange[i].TryGetComponent<Rigidbody>(out var rigidbody))
                {
                    var objectPos = objectsInRange[i].transform.position;
                    var forceDirection = (objectPos - projectilePosition).normalized;
                    var force = forceDirection * _projectileInfo.ExplosionForce + Vector3.up * _projectileInfo.ExplosionForce;
                    var position = projectilePosition + new Vector3(0, -0.5f, 0);
                    rigidbody.AddForceAtPosition(force, position, ForceMode.Impulse);
                }
            }
        }
    }
}
