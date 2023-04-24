using Photon.Pun;
using UI.Services;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public sealed class MainMenuUIInstaller : MonoInstaller
    {
        [field: SerializeField] public MainMenuCanvasView MainMenuCanvasView { get; private set; }
        [field: SerializeField] public MultiplayerView MultiplayerView { get; private set; }
        [field: SerializeField] public ServerSettings ServerSettings { get; private set; }
        [field: SerializeField] public RecordsWindowCanvasView RecordsWindowCanvasView { get; private set; }
        [field: SerializeField] public SettingsWindowCanvasView SettingsWindowCanvasView { get; private set; }

        public override void InstallBindings()
        {
            BindCanvases();
            BindMainMenuService();
            BindLobbyWindowService();
            BindRecordsWindowService();
            BindSettingsService();
        }

        private void BindCanvases()
        {
            Container
                .Bind<MainMenuCanvasView>()
                .FromInstance(MainMenuCanvasView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<MultiplayerView>()
                .FromInstance(MultiplayerView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<RecordsWindowCanvasView>()
                .FromInstance(RecordsWindowCanvasView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<SettingsWindowCanvasView>()
                .FromInstance(SettingsWindowCanvasView)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindMainMenuService()
        {
            Container
                .BindInterfacesAndSelfTo<MainMenuService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindLobbyWindowService()
        {
            Container
                .Bind<ServerSettings>()
                .FromInstance(ServerSettings)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PhotonLobbyService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<LobbyWindowService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindRecordsWindowService()
        {
            Container
                .BindInterfacesAndSelfTo<RecordsWindowService>()
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