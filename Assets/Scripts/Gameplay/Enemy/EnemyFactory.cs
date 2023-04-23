using Gameplay.Input;
using Gameplay.Unit;
using Scriptables;
using Services;
using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public sealed class EnemyFactory : PlaceholderFactory<Vector3, Enemy>
    {
        private readonly DiContainer _diContainer;
        private readonly Updater _updater;
        private readonly EnemyInput _enemyInput;
        private readonly EnemyConfig _enemyConfig;
        private readonly UnitHealthFactory _unitHealthFactory;
        private readonly UnitAbilitiesFactory _unitAbilitiesFactory;

        public event Action<Enemy> EnemyCreated = _ => { };

        public EnemyFactory(
            DiContainer diContainer,
            Updater updater,
            EnemyInput enemyInput,
            EnemyConfig enemyConfig,
            UnitHealthFactory unitHealthFactory,
            UnitAbilitiesFactory unitAbilitiesFactory)
        {
            _diContainer = diContainer;
            _updater = updater;
            _enemyInput = enemyInput;
            _enemyConfig = enemyConfig;
            _unitHealthFactory = unitHealthFactory;
            _unitAbilitiesFactory = unitAbilitiesFactory;
        }

        public override Enemy Create(Vector3 position)
        {
            var view = _diContainer.InstantiatePrefabForComponent<EnemyView>(_enemyConfig.EnemyView);
            view.transform.position = position;
            
            var unitHealth = _unitHealthFactory.Create(view, _enemyConfig.Health);
            var unitAbilities = _unitAbilitiesFactory.Create(view, _enemyInput, _enemyConfig.UnitAbilitiesConfig);

            var player = new Enemy(_updater, view, unitHealth, unitAbilities);
            EnemyCreated(player);
            return player;
        }
    }
}