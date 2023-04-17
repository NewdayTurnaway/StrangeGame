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
            _sceneLoader.LoadSceneAsync(SceneName.SINGLEPLAYER);
        }

        public void StartMultiplayerGame()
        {
            if (CurrentState == GameState.Multiplayer) return;
            CurrentState = GameState.Multiplayer;
            _sceneLoader.LoadSceneAsync(SceneName.MULTIPLAYER);
        }

        public void GoToLogin()
        {
            if (CurrentState == GameState.Login) return;
            CurrentState = GameState.Login;
            _sceneLoader.LoadSceneAsync(SceneName.LOGIN);
        }
        
        public void GoToMenu()
        {
            if (CurrentState == GameState.MainMenu) return;
            CurrentState = GameState.MainMenu;
            _sceneLoader.LoadSceneAsync(SceneName.MAIN_MENU);
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