using Gameplay.Input;
using Gameplay.Level;
using Scriptables;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyPassiveRoamingBehaviourFactory : PlaceholderFactory<EnemyView, EnemyInput, LevelPartView, EnemyConfig, EnemyPassiveRoamingBehaviour>
    {
    }
}