using Gameplay.Player;
using Services;
using System;

namespace UI.Services
{
    public sealed class PlayerInfoService : IDisposable
    {
        private readonly PlayerInfoView _playerInfoView;
        private readonly PlayerDataService _playerDataService;
        private readonly PlayerFactory _playerFactory;

        private Player _player;

        public PlayerInfoService(PlayerInfoView playerInfoView, PlayerDataService playerDataService, PlayerFactory playerFactory)
        {
            _playerInfoView = playerInfoView;
            _playerDataService = playerDataService;
            _playerFactory = playerFactory;

            _playerInfoView.ShowCanvas(false);
            
            _playerInfoView.PlayerName.text = _playerDataService.PlayerName;

            _playerInfoView.FristAbilityIcon.color = _playerInfoView.ColorNotActive;
            _playerInfoView.SecondAbilityIcon.color = _playerInfoView.ColorNotActive;
            _playerInfoView.FristAbilityCount.text = "";
            _playerInfoView.SecondAbilityCount.text = "";

            _playerFactory.PlayerCreated += OnPlayerCreated;
        }

        public void Dispose()
        {
            _playerFactory.PlayerCreated -= OnPlayerCreated;
            _player.UnitHealth.HealthChanged -= OnHealthChanged;

            if (_player.UnitAbilities.FirstProjectileAbility != null)
            {
                _player.UnitAbilities.FirstProjectileAbility.TotalThrowsChanged -= OnFirstTotalThrowsChanged;
                _player.UnitAbilities.FirstProjectileAbility.AbilityAvailable -= OnFirstAbilityAvailable;
            }
            if (_player.UnitAbilities.SecondProjectileAbility != null)
            {
                _player.UnitAbilities.SecondProjectileAbility.TotalThrowsChanged -= OnSecondTotalThrowsChanged;
                _player.UnitAbilities.SecondProjectileAbility.AbilityAvailable -= OnSecondAbilityAvailable;
            }
        }

        private void OnPlayerCreated(Player player)
        {
            _player = player;

            _playerInfoView.HealthSlider.minValue = 0;
            _playerInfoView.HealthSlider.maxValue = _player.UnitHealth.Health;
            _playerInfoView.HealthSlider.value = _player.UnitHealth.Health;

            _player.UnitHealth.HealthChanged += OnHealthChanged;

            if (_player.UnitAbilities.FirstProjectileAbility != null)
            {
                _playerInfoView.FristAbilityIcon.color = _playerInfoView.ColorActive;
                _playerInfoView.FristAbilityCount.text = _player.UnitAbilities.FirstProjectileAbility.CurrentTotalThrows.ToString();
                _player.UnitAbilities.FirstProjectileAbility.TotalThrowsChanged += OnFirstTotalThrowsChanged;
                _player.UnitAbilities.FirstProjectileAbility.AbilityAvailable += OnFirstAbilityAvailable;
            }
            if (_player.UnitAbilities.SecondProjectileAbility != null)
            {
                _playerInfoView.SecondAbilityIcon.color = _playerInfoView.ColorActive;
                _playerInfoView.SecondAbilityCount.text = _player.UnitAbilities.SecondProjectileAbility.CurrentTotalThrows.ToString();
                _player.UnitAbilities.SecondProjectileAbility.TotalThrowsChanged += OnSecondTotalThrowsChanged;
                _player.UnitAbilities.SecondProjectileAbility.AbilityAvailable += OnSecondAbilityAvailable;
            }

            _playerInfoView.ShowCanvas(true);
        }

        private void OnHealthChanged()
        {
            _playerInfoView.HealthSlider.value = _player.UnitHealth.Health;
        }

        private void OnFirstAbilityAvailable()
        {
            _playerInfoView.FristAbilityIcon.color = _playerInfoView.ColorActive;
        }
        
        private void OnSecondAbilityAvailable()
        {
            _playerInfoView.SecondAbilityIcon.color = _playerInfoView.ColorActive;
        }

        private void OnFirstTotalThrowsChanged()
        {
            _playerInfoView.FristAbilityIcon.color = _playerInfoView.ColorNotActive;
            _playerInfoView.FristAbilityCount.text = _player.UnitAbilities.FirstProjectileAbility.CurrentTotalThrows.ToString();
        }
        
        private void OnSecondTotalThrowsChanged()
        {
            _playerInfoView.SecondAbilityIcon.color = _playerInfoView.ColorNotActive;
            _playerInfoView.SecondAbilityCount.text = _player.UnitAbilities.SecondProjectileAbility.CurrentTotalThrows.ToString();
        }
    }
}
