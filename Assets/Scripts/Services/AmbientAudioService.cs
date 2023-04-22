using Services.SceneLoader;
using System;
using UnityEngine;
using Zenject;

namespace Services
{
    public sealed class AmbientAudioService : IInitializable, IDisposable
    {
        private readonly AudioSource _ambient;
        private readonly SceneLoader.SceneLoader _sceneLoader;

        private string _lastSceneName;

        public AmbientAudioService(AudioSource ambient, SceneLoader.SceneLoader sceneLoader)
        {
            _ambient = ambient;
            _sceneLoader = sceneLoader;
            _sceneLoader.SceneLoaded += OnSceneLoaded;
        }

        public void Initialize()
        {
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
            
            switch (_lastSceneName)
            {
                case SceneName.SINGLEPLAYER:
                    _ambient.Play();
                    break;
                case SceneName.MULTIPLAYER:
                    _ambient.Play();
                    break;
            }

            _lastSceneName = sceneName;
        }
    }
}