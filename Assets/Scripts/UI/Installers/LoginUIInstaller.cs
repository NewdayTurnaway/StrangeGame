using System;
using UI.Services;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public sealed class LoginUIInstaller : MonoInstaller
    {
        [field: SerializeField] public AuthorizationCanvasView AuthorizationCanvasView { get; private set; }
        [field: SerializeField] public SignInCanvasView SignInCanvasView { get; private set; }
        [field: SerializeField] public CreateAccountCanvasView CreateAccountCanvasView { get; private set; }

        public override void InstallBindings()
        {
            BindCanvases();
            BindAuthorizationService();
        }

        private void BindCanvases()
        {
            Container
                .Bind<AuthorizationCanvasView>()
                .FromInstance(AuthorizationCanvasView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<SignInCanvasView>()
                .FromInstance(SignInCanvasView)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<CreateAccountCanvasView>()
                .FromInstance(CreateAccountCanvasView)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindAuthorizationService()
        {
            Container
                .BindInterfacesAndSelfTo<AuthorizationService>()
                .AsSingle()
                .NonLazy();
        }
    } 
}