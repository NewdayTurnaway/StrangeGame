using Services.SceneLoader;
using System;
using UnityEngine;

namespace Services
{
    public sealed class AmbientAudioService : IDisposable
    {
        private readonly AudioSource _ambient;
        private readonly SceneLoader.SceneLoader _sceneLoader;

        public AmbientAudioService(AudioSource ambient, SceneLoader.SceneLoader sceneLoader)
        {
            _ambient = ambient;
            _sceneLoader = sceneLoader;
            _sceneLoader.SceneLoaded += OnSceneLoaded;
            OnSceneLoaded(_sceneLoader.StartScene);
        }

        public void Dispose()
        {
            _sceneLoader.SceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(string sceneName)
        {
            switch (sceneName)
            {
                case SceneName.SINGLEPLAYER:
                    _ambient.Stop();
                    break;
                case SceneName.MULTIPLAYER:
                    _ambient.Stop();
                    break;
            }
        }
    }
}