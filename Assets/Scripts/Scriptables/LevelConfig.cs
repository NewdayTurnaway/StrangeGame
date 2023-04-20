using Gameplay.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Configs/Level/" + nameof(LevelConfig))]
    public sealed class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public List<LevelPartView> LevelPartViews { get; private set; }
        [field: SerializeField, Range(1, 10)] public int LevelPartCount { get; private set; } = 5;
        [field: SerializeField, Min(0)] public int LevelPartSize { get; private set; } = 125;
    }
}