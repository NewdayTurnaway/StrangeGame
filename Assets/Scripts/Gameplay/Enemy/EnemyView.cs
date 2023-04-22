using Gameplay.Unit;
using UnityEngine;

namespace Gameplay.Enemy
{
    public sealed class EnemyView : UnitView
    {
        public override UnitType UnitType => UnitType.Enemy;

        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform Orientation { get; private set; }
    }
}
