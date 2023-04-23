using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Player;
using Scriptables;
using System;

namespace Gameplay.Enemy
{
    public sealed class EnemyBehaviourSwitcher : IDisposable
    {
        private readonly EnemyView _view;
        private readonly EnemyInput _enemyInput;
        private readonly LevelPartView _levelPartView;
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyPassiveRoamingBehaviourFactory _passiveRoamingBehaviourFactory;
        private readonly EnemyInCombatBehaviourFactory _inCombatBehaviourFactory;

        public EnemyBehaviour CurrentBehaviour { get; private set; }

        public EnemyBehaviourSwitcher(
            EnemyView view,
            EnemyInput enemyInput,
            LevelPartView levelPartView,
            EnemyConfig enemyConfig,
            EnemyPassiveRoamingBehaviourFactory passiveRoamingBehaviourFactory,
            EnemyInCombatBehaviourFactory inCombatBehaviourFactory
            )
        {
            _view = view;
            _enemyInput = enemyInput;
            _levelPartView = levelPartView;
            _enemyConfig = enemyConfig;
            _passiveRoamingBehaviourFactory = passiveRoamingBehaviourFactory;
            _inCombatBehaviourFactory = inCombatBehaviourFactory;

            _levelPartView.PlayerViewRecived += StartCombat;
            _levelPartView.PlayerLeftThisLevelPart += StartPassiveRoaming;

            StartPassiveRoaming();
        }

        public void Dispose()
        {
            _levelPartView.PlayerViewRecived -= StartCombat;
            _levelPartView.PlayerLeftThisLevelPart -= StartPassiveRoaming;
            CurrentBehaviour?.Dispose();
        }

        private void StartCombat(PlayerView playerView)
        {
            CurrentBehaviour?.Dispose();

            CurrentBehaviour = _inCombatBehaviourFactory.Create(_view, _enemyInput, _levelPartView, _enemyConfig, playerView);
        }

        private void StartPassiveRoaming()
        {
            CurrentBehaviour?.Dispose();

            CurrentBehaviour = _passiveRoamingBehaviourFactory.Create(_view, _enemyInput, _levelPartView, _enemyConfig);
        }
    }
}