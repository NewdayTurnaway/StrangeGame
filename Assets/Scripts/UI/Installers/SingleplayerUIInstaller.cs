using UI.Services;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public sealed class SingleplayerUIInstaller : MonoInstaller
    {
        [field: SerializeField] public PauseCanvasView PauseCanvasView { get; private set; }
        [field: SerializeField] public SettingsWindowCanvasView SettingsWindowCanvasView { get; private set; }

        public override void InstallBindings()
        {
            BindCanvases();
            BindPauseService();
            BindSettingsService();
        }

        private void BindCanvases()
        {
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