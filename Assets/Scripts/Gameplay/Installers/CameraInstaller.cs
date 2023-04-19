using Gameplay.Services;
using Scriptables;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public sealed class CameraInstaller : MonoInstaller
    {
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
        [field: SerializeField] public Camera MainCamera { get; private set; }

        public override void InstallBindings()
        {
            InstallGameCamera();
        }

        private void InstallGameCamera()
        {
            Container
                .Bind<CameraConfig>()
                .FromInstance(CameraConfig)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Camera>()
                .FromInstance(MainCamera)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameCameraService>()
                .AsSingle()
                .NonLazy();
        }
    }
}