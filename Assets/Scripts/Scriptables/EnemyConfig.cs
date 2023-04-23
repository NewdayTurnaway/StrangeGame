using Gameplay.Enemy;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/Enemy/" + nameof(EnemyConfig))]
    public sealed class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyView EnemyView { get; private set; }
        [field: SerializeField, Min(1)] public float Health { get; private set; } = 100f;

        [field: Header("Movement")]
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 10f;
        [field: SerializeField, Min(0)] public float SpeedMiltiplier { get; private set; } = 10f;
        [field: SerializeField, Min(0)] public float JumpForce { get; private set; } = 12f;
        [field: SerializeField, Min(0)] public float JumpCooldown { get; private set; } = 0.25f;
        [field: SerializeField, Min(0)] public float AirMiltiplier { get; private set; } = 0.4f;


        [field: Header("Ground Check")]
        [field: SerializeField, Min(0)] public float PlayerHeight { get; private set; } = 2f;
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
        [field: SerializeField, Min(0)] public float GroundDrag { get; private set; } = 5f;

        [field: Space(20)]
        [field: Header("Other")]
        [field: SerializeField] public UnitAbilitiesConfig UnitAbilitiesConfig { get; private set; }
    }
}