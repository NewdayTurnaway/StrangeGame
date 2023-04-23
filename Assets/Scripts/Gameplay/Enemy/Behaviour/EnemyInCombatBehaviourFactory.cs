using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Player;
using Scriptables;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyInCombatBehaviourFactory : PlaceholderFactory<EnemyView, EnemyInput, LevelPartView, EnemyConfig, PlayerView, EnemyInCombatBehaviour>
    {
    }
}