using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using Zenject;

namespace Services
{
    public sealed class PlayFabService : IInitializable
    {
        private const string PLAY_FAB_TITLE_ID = "D61B6";
        private readonly SceneLoader.SceneLoader _sceneLoader;
        private readonly LoadingUIService _loadingUIService;
        private readonly PlayerDataService _playerDataService;

        public event  Action<string> Error = _ => { };

        public PlayFabService(SceneLoader.SceneLoader sceneLoader, LoadingUIService loadingUIService, PlayerDataService playerDataService)
        {
            _sceneLoader = sceneLoader;
            _loadingUIService = loadingUIService;
            _playerDataService = playerDataService;
        }

        public void Initialize()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            {
                PlayFabSettings.staticSettings.TitleId = PLAY_FAB_TITLE_ID;
            }
        }

        public void SignIn(string email, string password)
        {
            _loadingUIService.ShowLoading();

            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, 
                resultCallback => 
                {
                    _playerDataService.ChangeLoginStatus(true);
                    _sceneLoader.LoadSceneAsync(SceneLoader.SceneName.MAIN_MENU);
                }, OnError);
        }

        public void CreateAccount(string email, string username, string password)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = email,
                Username = username,
                Password = password,
                RequireBothUsernameAndEmail = true
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, 
                resultCallback =>
                {
                    _playerDataService.SetPlayer(username, email, password);
                    SignIn(email, password);
                }, OnError);
        }

        private void OnError(PlayFabError error)
        {
            _loadingUIService.HideLoading();
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError($"Something went wrong: {errorMessage}");
            Error.Invoke(errorMessage);
        }
    }
}