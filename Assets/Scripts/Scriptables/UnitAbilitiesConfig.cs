using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(UnitAbilitiesConfig), menuName = "Configs/Unit/" + nameof(UnitAbilitiesConfig))]
    public sealed class UnitAbilitiesConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileConfig FirstProjectileAbility { get; private set; }
        [field: SerializeField] public ProjectileConfig SecondProjectileAbility { get; private set; }
    }
}