using Services;
using Zenject;

namespace UI.Services
{
    public sealed class MainMenuService : IInitializable
    {
        private readonly GameStateService _gameStateService;
        private readonly PlayerDataService _playerDataService;
        private readonly MainMenuCanvasView _mainMenuCanvasView;
        private readonly MultiplayerView _multiplayerView;
        private readonly RecordsWindowCanvasView _recordsWindowCanvasView;
        private readonly SettingsWindowCanvasView _settingsWindowCanvasView;

        public MainMenuService(
            GameStateService gameStateService,
            PlayerDataService playerDataService,
            MainMenuCanvasView mainMenuCanvasView,
            MultiplayerView multiplayerView,
            RecordsWindowCanvasView recordsWindowCanvasView,
            SettingsWindowCanvasView settingsWindowCanvasView
            )
        {
            _gameStateService = gameStateService;
            _playerDataService = playerDataService;
            _mainMenuCanvasView = mainMenuCanvasView;
            _multiplayerView = multiplayerView;
            _recordsWindowCanvasView = recordsWindowCanvasView;
            _settingsWindowCanvasView = settingsWindowCanvasView;
        }

        public void Initialize()
        {
            _mainMenuCanvasView.Init(
                _gameStateService.StartSingleplayerGame,
                OpenMultiplayerWindow,
                OpenRecordsWindow,
                OpenSettingsWindow,
                SignOut,
                _gameStateService.ExitGame);
            _mainMenuCanvasView.ShowCanvas(true);
        }

        private void OpenMultiplayerWindow()
        {
            _multiplayerView.LobbyWindowCanvasView.ShowCanvas(true);
        }

        private void OpenRecordsWindow()
        {
            _recordsWindowCanvasView.ShowCanvas(true);
        }

        private void OpenSettingsWindow()
        {
            _settingsWindowCanvasView.ShowCanvas(true);
        }

        private void SignOut()
        {
            _playerDataService.ForgetPlayer();
            _gameStateService.GoToLogin();
        }
    }
}
