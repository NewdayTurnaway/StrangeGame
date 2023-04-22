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

        public override void InstallBindings()
        {
            BindViews();
            BindHUD();
            BindPauseService();
            BindSettingsService();
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
    } 
}