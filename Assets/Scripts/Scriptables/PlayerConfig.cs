using Gameplay.Player;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerView PlayerView { get; private set; }
        
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
    }
}