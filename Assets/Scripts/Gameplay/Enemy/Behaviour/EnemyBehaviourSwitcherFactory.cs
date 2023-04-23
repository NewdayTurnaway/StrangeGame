using Gameplay.Input;
using Gameplay.Level;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyBehaviourSwitcherFactory : PlaceholderFactory<EnemyView, EnemyInput, LevelPartView, EnemyBehaviourSwitcher>
    {
    }
}