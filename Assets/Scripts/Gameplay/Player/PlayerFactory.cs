using Scriptables;
using Services;
using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public sealed class PlayerFactory : PlaceholderFactory<Vector3, Player>
    {
        private readonly DiContainer _diContainer;
        private readonly Updater _updater;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerMovementFactory _playerMovementFactory;

        public event Action<Player> PlayerCreated = _ => { };

        public PlayerFactory(DiContainer diContainer, Updater updater, PlayerConfig playerConfig, PlayerMovementFactory playerMovementFactory)
        {
            _diContainer = diContainer;
            _updater = updater;
            _playerConfig = playerConfig;
            _playerMovementFactory = playerMovementFactory;
        }

        public override Player Create(Vector3 position)
        {
            var view = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.PlayerView);
            view.transform.position = position;
            
            var movement = _playerMovementFactory.Create(view);
            
            var player = new Player(_updater, view, movement);
            PlayerCreated(player);
            return player;
        }
    }
}