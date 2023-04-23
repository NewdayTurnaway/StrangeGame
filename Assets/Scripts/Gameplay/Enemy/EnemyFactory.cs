using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Unit;
using Scriptables;
using Services;
using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyFactory : PlaceholderFactory<Vector3, LevelPartView, Enemy>
    {
        private readonly DiContainer _diContainer;
        private readonly Updater _updater;
        private readonly EnemyConfig _enemyConfig;
        private readonly EnemyInputFactory _enemyInputFactory;
        private readonly EnemyBehaviourSwitcherFactory _enemyBehaviourSwitcherFactory;
        private readonly EnemyMovementFactory _enemyMovementFactory;
        private readonly UnitHealthFactory _unitHealthFactory;
        private readonly UnitAbilitiesFactory _unitAbilitiesFactory;

        public event Action<Enemy> EnemyCreated = _ => { };

        public EnemyFactory(
            DiContainer diContainer,
            Updater updater,
            EnemyConfig enemyConfig,
            EnemyInputFactory enemyInputFactory,
            EnemyBehaviourSwitcherFactory enemyBehaviourSwitcherFactory,
            EnemyMovementFactory enemyMovementFactory,
            UnitHealthFactory unitHealthFactory,
            UnitAbilitiesFactory unitAbilitiesFactory)
        {
            _diContainer = diContainer;
            _updater = updater;
            _enemyConfig = enemyConfig;
            _enemyInputFactory = enemyInputFactory;
            _enemyBehaviourSwitcherFactory = enemyBehaviourSwitcherFactory;
            _enemyMovementFactory = enemyMovementFactory;
            _unitHealthFactory = unitHealthFactory;
            _unitAbilitiesFactory = unitAbilitiesFactory;
        }

        public override Enemy Create(Vector3 position, LevelPartView levelPartView)
        {
            var view = _diContainer.InstantiatePrefabForComponent<EnemyView>(_enemyConfig.EnemyView);
            view.transform.position = position;

            var enemyInput = _enemyInputFactory.Create();
            var enemyBehaviourSwitcher = _enemyBehaviourSwitcherFactory.Create(view, enemyInput, levelPartView);
            var movement = _enemyMovementFactory.Create(view, enemyInput);
            var unitHealth = _unitHealthFactory.Create(view, _enemyConfig.Health);
            var unitAbilities = _unitAbilitiesFactory.Create(view, enemyInput, _enemyConfig.UnitAbilitiesConfig);

            var player = new Enemy(_updater, view, enemyInput, enemyBehaviourSwitcher, movement, unitHealth, unitAbilities);
            EnemyCreated(player);
            return player;
        }
    }
}