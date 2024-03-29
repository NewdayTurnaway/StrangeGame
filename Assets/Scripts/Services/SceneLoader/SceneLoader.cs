using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public sealed class SceneLoader
    {
        private readonly LoadingUIService _loadingUIService;

        public string StartScene { get; private set; }

        public Action<string> SceneLoaded = _ => { };

        public SceneLoader(LoadingUIService loadingUIService)
        {
            _loadingUIService = loadingUIService;
            StartScene = SceneManager.GetActiveScene().name;
        }

        public async void LoadSceneAsync(string sceneName)
        {
            _loadingUIService.ShowLoading();

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (asyncOperation.isDone == false)
            {
                await Task.Yield();
            }

            _loadingUIService.HideLoading();

            SceneLoaded.Invoke(sceneName);
        }
    }
}