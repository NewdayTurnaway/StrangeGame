using Gameplay.Input;
using Gameplay.Mechanics.Timer;
using Scriptables;
using System;

namespace Gameplay.Unit
{
    public sealed class UnitAbilities : IDisposable
    {
        private readonly TimerFactory _timerFactory;
        private readonly UnitView _unitView;
        private readonly PlayerInput _playerInput;
        private readonly UnitAbilitiesConfig _unitAbilitiesConfig;
        private readonly ProjectileAbilityFactory _projectileAbilityFactory;

        private readonly ProjectileAbility _firstProjectileAbility;
        private readonly ProjectileAbility _secondProjectileAbility;

        public ProjectileAbility FirstProjectileAbility => _firstProjectileAbility;
        public ProjectileAbility SecondProjectileAbility => _secondProjectileAbility;

        public UnitAbilities(
            TimerFactory timerFactory,
            UnitView unitView,
            PlayerInput playerInput,
            UnitAbilitiesConfig unitAbilitiesConfig,
            ProjectileAbilityFactory projectileAbilityFactory)
        {
            _timerFactory = timerFactory;
            _unitView = unitView;
            _playerInput = playerInput;
            _unitAbilitiesConfig = unitAbilitiesConfig;
            _projectileAbilityFactory = projectileAbilityFactory;

            if (_unitAbilitiesConfig.FirstProjectileAbility != null)
            {
                _firstProjectileAbility = CreateProjectileAbility(_unitAbilitiesConfig.FirstProjectileAbility);
                _playerInput.FirstAbilityInput += OnFirstAbilityInput;
            }

            if (_unitAbilitiesConfig.SecondProjectileAbility != null)
            {
                _secondProjectileAbility = CreateProjectileAbility(_unitAbilitiesConfig.SecondProjectileAbility);
                _playerInput.SecondAbilityInput += OnSecondAbilityInput;
            }
        }

        

        public void Dispose()
        {
            if (_firstProjectileAbility != null)
            {
                _playerInput.FirstAbilityInput -= OnFirstAbilityInput;
                _firstProjectileAbility.Dispose(); 
            }
            
            if (_secondProjectileAbility != null)
            {
                _playerInput.SecondAbilityInput -= OnSecondAbilityInput;
                _secondProjectileAbility.Dispose(); 
            }
        }

        private ProjectileAbility CreateProjectileAbility(ProjectileConfig projectileConfig)
        {
            var timer = _timerFactory.Create(projectileConfig.ThrowCooldown);
            var projectileAbility = _projectileAbilityFactory.Create(_unitView, projectileConfig, timer);
            return projectileAbility;
        }

        private void OnFirstAbilityInput(bool abilityInput)
        {
            _firstProjectileAbility.ApplyAbility(abilityInput);
        }

        private void OnSecondAbilityInput(bool abilityInput)
        {
            _secondProjectileAbility.ApplyAbility(abilityInput);
        }
    }
}