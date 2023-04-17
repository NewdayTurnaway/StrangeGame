using Core.GameState;
using Services.SceneLoader;

namespace Services
{
    public sealed class GameStateService
    {
        private readonly SceneLoader.SceneLoader _sceneLoader;
        public GameState CurrentState { get; private set; }

        public GameStateService(SceneLoader.SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            CurrentState = GameState.MainMenu;
        }

        public void StartSingleplayerGame()
        {
            if (CurrentState == GameState.Singleplayer) return;
            CurrentState = GameState.Singleplayer;
            _sceneLoader.LoadScene(SceneName.SINGLEPLAYER);
        }

        public void StartMultiplayerGame()
        {
            if (CurrentState == GameState.Multiplayer) return;
            CurrentState = GameState.Multiplayer;
            _sceneLoader.LoadScene(SceneName.MULTIPLAYER);
        }

        public void GoToLogin()
        {
            if (CurrentState == GameState.Login) return;
            CurrentState = GameState.Login;
            _sceneLoader.LoadScene(SceneName.LOGIN);
        }
        
        public void GoToMenu()
        {
            if (CurrentState == GameState.MainMenu) return;
            CurrentState = GameState.MainMenu;
            _sceneLoader.LoadScene(SceneName.MAIN_MENU);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            UnityEngine.Application.Quit();
        }
    }
}