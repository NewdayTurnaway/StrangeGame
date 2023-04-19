using Gameplay.Input;
using Gameplay.Mechanics.Meter;
using Gameplay.Mechanics.Timer;
using Gameplay.Services;
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
                .Bind<CurrentGameState>()
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
    }
}