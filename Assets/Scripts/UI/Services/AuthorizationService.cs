using Services;
using System;
using TMPro;
using Zenject;

namespace UI.Services
{
    public sealed class AuthorizationService : IInitializable, IDisposable
    {
        private const string PASSWORD_ERROR = "Password too short!";

        private readonly PlayerDataService _playerDataService;
        private readonly PlayFabService _playFabService;
        private readonly AuthorizationCanvasView _authorizationCanvasView;
        private readonly SignInCanvasView _signInCanvasView;
        private readonly CreateAccountCanvasView _createAccountCanvasView;
        private readonly ErrorCanvasView _errorCanvasView;

        public AuthorizationService(
            PlayerDataService playerDataService,
            PlayFabService playFabService,
            AuthorizationCanvasView authorizationCanvasView,
            SignInCanvasView signInCanvasView,
            CreateAccountCanvasView createAccountCanvasView,
            ErrorCanvasView errorCanvasView)
        {
            _playerDataService = playerDataService;
            _playFabService = playFabService;
            _authorizationCanvasView = authorizationCanvasView;
            _signInCanvasView = signInCanvasView;
            _createAccountCanvasView = createAccountCanvasView;
            _errorCanvasView = errorCanvasView;
        }

        public void Initialize()
        {
            _errorCanvasView.ShowCanvas(false);
            _playFabService.Error += OnError; 
            _authorizationCanvasView.Init(OpenSignInWindow, OpenCreateAccountWindow);
            _signInCanvasView.Init(CloseSignInWindow, SignIn);
            _createAccountCanvasView.Init(CloseCreateAccountWindow, CreateAccount);

            if (string.IsNullOrWhiteSpace(_playerDataService.PlayerName))
            {
                _authorizationCanvasView.ShowCanvas(true);
            }
            else
            {
                _signInCanvasView.SetInputs(_playerDataService.PlayerEmail, _playerDataService.PlayerPassword);
                OpenSignInWindow();
            }
        }

        public void Dispose()
        {
            _playFabService.Error -= OnError;
        }

        private void OnError(string errorMessage)
        {
            _errorCanvasView.SetErrorMessage(errorMessage);
            _errorCanvasView.ShowCanvas(true);
        }

        private void OpenSignInWindow()
        {
            _signInCanvasView.ShowCanvas(true);
            _authorizationCanvasView.ShowCanvas(false);
            _errorCanvasView.ShowCanvas(false);
        }

        private void OpenCreateAccountWindow()
        {
            _createAccountCanvasView.ShowCanvas(true);
            _authorizationCanvasView.ShowCanvas(false);
            _errorCanvasView.ShowCanvas(false);
        }

        private void CloseSignInWindow()
        {
            _signInCanvasView.ShowCanvas(false);
            _authorizationCanvasView.ShowCanvas(true);
            _errorCanvasView.ShowCanvas(false);
        }

        private void CloseCreateAccountWindow()
        {
            _createAccountCanvasView.ShowCanvas(false);
            _authorizationCanvasView.ShowCanvas(true);
            _errorCanvasView.ShowCanvas(false);
        }
        
        private void SignIn()
        {
            var email = _signInCanvasView.EmailInputField.text;
            var password = _signInCanvasView.PasswordInputField.text;

            if (!CheckPasswordCorrect(password, _signInCanvasView.PasswordInputField)) return;

            _playFabService.SignIn(email, password);
        }
        
        private void CreateAccount()
        {
            var email = _createAccountCanvasView.EmailInputField.text;
            var username = _createAccountCanvasView.UsernameInputField.text;
            var password = _createAccountCanvasView.PasswordInputField.text;

            if (!CheckPasswordCorrect(password, _createAccountCanvasView.PasswordInputField)) return;

            _playFabService.CreateAccount(email, username, password);
        }

        private bool CheckPasswordCorrect(string password, TMP_InputField inputField)
        {
            if (password.Length < 6)
            {
                _signInCanvasView.PasswordInputField.text = "";
                var placeholder = (TextMeshProUGUI)inputField.placeholder;
                placeholder.text = PASSWORD_ERROR;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
