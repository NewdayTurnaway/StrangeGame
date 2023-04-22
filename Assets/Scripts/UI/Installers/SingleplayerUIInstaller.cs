using Gameplay.Level;
using Services;
using UI.Services;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public sealed class SingleplayerUIInstaller : MonoInstaller
    {
        [field: SerializeField] public PlayerInfoView PlayerInfoView { get; private set; }
        [field: SerializeField] public LevelProgressInfoView LevelProgressInfoView { get; private set; }
        [field: SerializeField] public PauseCanvasView PauseCanvasView { get; private set; }
        [field: SerializeField] public SettingsWindowCanvasView SettingsWindowCanvasView { get; private set; }
        [field: SerializeField] public EndScreenCanvasView EndScreenCanvasView { get; private set; }

        public override void InstallBindings()
        {
            BindViews();
            BindStatisticServiceFactory();
            BindHUD();
            BindPauseService();
            BindSettingsService();
            BindEndScreenService();
        }

        private void BindViews()
        {
            Container
                .Bind<PlayerInfoView>()
                .FromInstance(PlayerInfoView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<LevelProgressInfoView>()
                .FromInstance(LevelProgressInfoView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PauseCanvasView>()
                .FromInstance(PauseCanvasView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<SettingsWindowCanvasView>()
                .FromInstance(SettingsWindowCanvasView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<EndScreenCanvasView>()
                .FromInstance(EndScreenCanvasView)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindStatisticServiceFactory()
        {
            Container
                .BindFactory<LevelStatsView, StatisticService, StatisticServiceFactory>()
                .AsSingle();
        }
        
        private void BindHUD()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerInfoService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<LevelProgressInfoService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPauseService()
        {
            Container
                .BindInterfacesAndSelfTo<PauseService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindSettingsService()
        {
            Container
                .BindInterfacesAndSelfTo<SettingsService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEndScreenService()
        {
            Container
                .BindInterfacesAndSelfTo<EndScreenService>()
                .AsSingle()
                .NonLazy();
        }
    } 
}