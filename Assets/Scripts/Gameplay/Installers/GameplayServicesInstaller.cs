using Gameplay.Input;
using Gameplay.Mechanics.Meter;
using Gameplay.Mechanics.Timer;
using Gameplay.Services;
using Gameplay.Unit;
using Scriptables;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class GameplayServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallGameplayMechanics();
            InstallCurrentGameState();
            InstallPlayerInput();
            InstallUnitHealthFactory();
            InstallUnitAbilities();
        }

        private void InstallGameplayMechanics()
        {
            Container
                .BindFactory<float, Timer, TimerFactory>()
                .AsSingle();

            Container
                .BindFactory<float, float, float, MeterWithCooldown, MeterWithCooldownFactory>()
                .AsSingle();
        }

        private void InstallCurrentGameState()
        {
            Container
                .BindInterfacesAndSelfTo<CurrentGameState>()
                .AsSingle()
                .NonLazy();
        }
        
        private void InstallPlayerInput()
        {
            Container
                .Bind<PlayerInput>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallUnitHealthFactory()
        {
            Container
                 .BindFactory<UnitView, float, UnitHealth, UnitHealthFactory>()
                 .AsSingle();
        }
        
        private void InstallUnitAbilities()
        {
            Container
                 .BindFactory<UnitView, ProjectileConfig, Timer, ProjectileAbility, ProjectileAbilityFactory>()
                 .AsCached();
            
            Container
                 .BindFactory<UnitView, UnitAbilitiesConfig, UnitAbilities, UnitAbilitiesFactory>()
                 .AsCached();
        }
    }
}