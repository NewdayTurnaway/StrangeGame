using Services;
using Services.SceneLoader;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class GlobalsInstaller : MonoInstaller
    {
        [field: SerializeField] public LoadingCanvasView LoadingCanvasView { get; private set; }

        public override void InstallBindings()
        {
            BindLoadingUIService();
            BindSceneLoader();
            BindGameState();
            BindPlayerData();
            BindPlayFab();
            BindUpdater();
            
            Debug.Log("Global!");
        }

        private void BindLoadingUIService()
        {
            Container
                .Bind<LoadingCanvasView>()
                .FromInstance(LoadingCanvasView)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<LoadingUIService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameState()
        {
            Container
                .Bind<GameStateService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerData()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerDataService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayFab()
        {
            Container
                .BindInterfacesAndSelfTo<PlayFabService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindUpdater()
        {
            Container
                .BindInterfacesAndSelfTo<Updater>()
                .AsSingle()
                .NonLazy();
        }
    }
}