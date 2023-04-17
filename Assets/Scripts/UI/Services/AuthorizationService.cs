using Services;
using Zenject;

namespace UI.Services
{
    public sealed class AuthorizationService : IInitializable
    {
        private readonly PlayerDataService _playerDataService;
        private readonly AuthorizationCanvasView _authorizationCanvasView;
        private readonly SignInCanvasView _signInCanvasView;
        private readonly CreateAccountCanvasView _createAccountCanvasView;

        public AuthorizationService(PlayerDataService playerDataService, AuthorizationCanvasView authorizationCanvasView, SignInCanvasView signInCanvasView, CreateAccountCanvasView createAccountCanvasView)
        {
            _playerDataService = playerDataService;
            _authorizationCanvasView = authorizationCanvasView;
            _signInCanvasView = signInCanvasView;
            _createAccountCanvasView = createAccountCanvasView;
        }

        public void Initialize()
        {
            _authorizationCanvasView.Init(OpenSignInWindow, OpenCreateAccountWindow);
            _signInCanvasView.Init(CloseSignInWindow);
            _createAccountCanvasView.Init(CloseCreateAccountWindow);

            if (string.IsNullOrWhiteSpace(_playerDataService.PlayerName))
            {
                _authorizationCanvasView.ShowCanvas(true);
            }
            else
            {
                _signInCanvasView.SetInputs(_playerDataService.PlayerEmail, _playerDataService.PlayerPassword);
                OpenSignInWindow();
                //PlayFab
            }
        }

        private void OpenSignInWindow()
        {
            _signInCanvasView.ShowCanvas(true);
            _authorizationCanvasView.ShowCanvas(false);
        }

        private void OpenCreateAccountWindow()
        {
            _createAccountCanvasView.ShowCanvas(true);
            _authorizationCanvasView.ShowCanvas(false);
        }

        private void CloseSignInWindow()
        {
            _signInCanvasView.ShowCanvas(false);
            _authorizationCanvasView.ShowCanvas(true);
        }

        private void CloseCreateAccountWindow()
        {
            _createAccountCanvasView.ShowCanvas(false);
            _authorizationCanvasView.ShowCanvas(true);
        }
    }
}
