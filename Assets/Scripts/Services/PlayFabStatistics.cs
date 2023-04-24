using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public sealed class PlayFabStatistics : IDisposable
    {
        public const string STATISTIC_NAME = "Score";
        private const int MAX_RESULTS_COUNT = 10;

        private readonly PlayerDataService _playerDataService;

        public event Action<StatisticValue> OnStatisticRetrieved = _ => { };
        public event Action<List<PlayerLeaderboardEntry>> OnLeaderboardRetrieved = _ => { };
        public event Action<string> Error = _ => { };

        public PlayFabStatistics(PlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;

            _playerDataService.UpdateStatistics += OnUpdateStatistics;
        }

        public void Dispose()
        {
            _playerDataService.UpdateStatistics -= OnUpdateStatistics;
        }

        public void GetStatistic()
        {
            if (!_playerDataService.IsLoginSuccess) return;

            PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest()
            {
                StatisticNames = new List<string> { STATISTIC_NAME }
            },
            resultCallback =>
            {
                if(resultCallback.Statistics != null && resultCallback.Statistics.Count > 0)
                {
                    OnStatisticRetrieved.Invoke(resultCallback.Statistics[0]);
                }
            }, OnError);
        }

        public void SetStatistic(int value)
        {
            if (!_playerDataService.IsLoginSuccess) return;

            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest()
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate()
                    {
                        StatisticName = STATISTIC_NAME,
                        Value = value
                    }
                }
            },
            resultCallback =>
            {
                Debug.Log("SetStatistic");
            }, OnError);
        }

        public void GetLeaderboard()
        {
            if (!_playerDataService.IsLoginSuccess) return;

            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest()
            {
                StatisticName = STATISTIC_NAME,
                MaxResultsCount = MAX_RESULTS_COUNT
            },
            resultCallback =>
            {
                if (resultCallback.Leaderboard != null)
                {
                    OnLeaderboardRetrieved.Invoke(resultCallback.Leaderboard);
                }
            }, OnError);
        }

        private void OnUpdateStatistics(int score)
        {
            SetStatistic(score);
        }

        private void OnError(PlayFabError error)
        {
            var errorMessage = error.GenerateErrorReport();
            Debug.LogError($"Something went wrong: {errorMessage}");
            Error.Invoke(errorMessage);
        }
    }
}