using Gameplay.Unit;

namespace Gameplay.Projectile
{
    public sealed class ProjectileInfo
    {
        public ProjectileType ProjectileType { get; private set; }
        public UnitType OwnerUnitType { get; private set; }
        public float Damage { get; private set; }
        public float LifeTime { get; private set; }
        public bool IsDestroyedOnHit { get; private set; }
        public float ExplosionRadius { get; private set; }
        public float ExplosionForce { get; private set; }

        public ProjectileInfo(
            ProjectileType projectileType,
            UnitType ownerUnitType,
            float damage,
            float lifeTime,
            bool isDestroyedOnHit,
            float explosionRadius,
            float explosionForce)
        {
            ProjectileType = projectileType;
            OwnerUnitType = ownerUnitType;
            Damage = damage;
            LifeTime = lifeTime;
            IsDestroyedOnHit = isDestroyedOnHit;
            ExplosionRadius = explosionRadius;
            ExplosionForce = explosionForce;
        }
    }
}