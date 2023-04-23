using Gameplay.Unit;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerView : UnitView
    {
        public override UnitType UnitType => UnitType.Player;
    }
}
