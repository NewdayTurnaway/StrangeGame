using Gameplay.Player;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerView PlayerView { get; private set; }
    }
}