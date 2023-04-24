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

        public PlayFabService(
            SceneLoader.SceneLoader sceneLoader,
            LoadingUIService loadingUIService,
            PlayerDataService playerDataService)
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
                    OnLoginSuccess(email, password);
                }, OnError);
        }

        private void OnLoginSuccess(string email, string password)
        {
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
                resultCallback =>
                {
                    OnGetAccountInfoSuccess(resultCallback.AccountInfo.PlayFabId, resultCallback.AccountInfo.Username, email, password);
                }, OnError);
        }
        
        private void OnGetAccountInfoSuccess(string playFabId, string username, string email, string password)
        {
            PlayFabClientAPI.GetLeaderboardAroundPlayer(new GetLeaderboardAroundPlayerRequest()
            {
                PlayFabId = playFabId, 
                StatisticName = PlayFabStatistics.STATISTIC_NAME, 
                MaxResultsCount = 1,
            },
            resultCallback =>
            {
                _playerDataService.ChangeLoginStatus(true);
                var playerLeaderboardEntry = resultCallback.Leaderboard[0];
                _playerDataService.SetPlayer(username, email, password, playerLeaderboardEntry.Position, playerLeaderboardEntry.StatValue);
                _sceneLoader.LoadSceneAsync(SceneLoader.SceneName.MAIN_MENU);
            }, OnError);
        }

        public void CreateAccount(string email, string username, string password)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = email,
                Username = username,
                DisplayName = username,
                Password = password,
                RequireBothUsernameAndEmail = true,
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, 
                resultCallback =>
                {
                    _playerDataService.SetPlayer(username, email, password, 0, 0);
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