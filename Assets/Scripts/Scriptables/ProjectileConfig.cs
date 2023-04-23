using Gameplay.Projectile;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(ProjectileConfig), menuName = "Configs/Projectile/" + nameof(ProjectileConfig))]
    public sealed class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileView ProjectileView { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; } = ProjectileType.NonExplosive;
        [field: SerializeField] public float Damage { get; private set; } = 10f;
        [field: SerializeField] public float LifeTime { get; private set; } = 10f;
        [field: SerializeField] public bool IsDestroyedOnHit { get; private set; } = true;

        [field: Header("NonExplosive Projectile")]
        [field: SerializeField, Min(0)] public int TotalThrows { get; private set; } = 10;
        [field: SerializeField, Min(0)] public float ThrowForce { get; private set; } = 70f;
        [field: SerializeField, Min(0)] public float ThrowUpwardForce { get; private set; } = 0f;
        [field: SerializeField, Min(0)] public float ThrowCooldown { get; private set; } = 0.1f;

        [field: Header("Explosive Projectile")]
        [field: SerializeField, Min(0)] public ParticleSystem ExplosionEffect { get; private set; }
        [field: SerializeField, Min(0)] public float ExplosionRadius { get; private set; } = 10f;
        [field: SerializeField, Min(0)] public float ExplosionForce { get; private set; } = 50f;
    }
}