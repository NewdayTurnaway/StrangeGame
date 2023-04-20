using Scriptables;
using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public sealed class PlayerFactory : PlaceholderFactory<Vector3, Player>
    {
        private readonly DiContainer _diContainer;
        private readonly PlayerConfig _playerConfig;

        public event Action<Player> PlayerCreated = _ => { };

        public PlayerFactory(DiContainer diContainer, PlayerConfig playerConfig)
        {
            _diContainer = diContainer;
            _playerConfig = playerConfig;
        }

        public override Player Create(Vector3 position)
        {
            var view = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.PlayerView);
            view.transform.position = position;
            
            var player = new Player(view);
            PlayerCreated(player);
            return player;
        }
    }
}