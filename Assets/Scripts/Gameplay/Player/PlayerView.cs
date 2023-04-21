using Gameplay.Unit;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerView : UnitView
    {
        public override UnitType UnitType => UnitType.Player;

        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform Orientation { get; private set; }
    }
}
