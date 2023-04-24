using PlayFab.ClientModels;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Services
{
    public sealed class RecordsWindowService : IDisposable
    {
        private readonly LoadingUIService _loadingUIService;
        private readonly PlayerDataService _playerDataService;
        private readonly PlayFabStatistics _playFabStatistics;
        private readonly RecordsWindowCanvasView _recordsWindowCanvasView;

        public RecordsWindowService(
            LoadingUIService loadingUIService,
            PlayerDataService playerDataService,
            PlayFabStatistics playFabStatistics,
            MainMenuCanvasView mainMenuCanvasView,
            RecordsWindowCanvasView recordsWindowCanvasView
            )
        {
            _loadingUIService = loadingUIService;
            _playerDataService = playerDataService;
            _playFabStatistics = playFabStatistics;
            _recordsWindowCanvasView = recordsWindowCanvasView;

            if (!_playerDataService.IsLoginSuccess)
            {
                mainMenuCanvasView.RecordsButton.interactable = false;
                return;
            }

            _recordsWindowCanvasView.PlayerRecordContainerView.Init(_playerDataService.RecordPosition, _playerDataService.PlayerName, _playerDataService.RecordScore);
            _recordsWindowCanvasView.RefreshButton.onClick.AddListener(RefreshLeaderboard);

            _playFabStatistics.OnLeaderboardRetrieved += ShowLeaderboard;
            RefreshLeaderboard();
        }

        public void Dispose()
        {
            _playFabStatistics.OnLeaderboardRetrieved -= ShowLeaderboard;
            _recordsWindowCanvasView.RefreshButton.onClick.RemoveListener(RefreshLeaderboard);
        }

        private void RefreshLeaderboard()
        {
            _loadingUIService.ShowLoading();

            _playFabStatistics.GetLeaderboard();
        }

        private void ShowLeaderboard(List<PlayerLeaderboardEntry> leaderboardEntries)
        {
            foreach (RectTransform child in _recordsWindowCanvasView.ContentRectTransform)
            {
                Object.Destroy(child.gameObject);
            }

            foreach (var leaderboardEntry in leaderboardEntries)
            {
                var recordContainer = Object.Instantiate(_recordsWindowCanvasView.RecordContainerView, _recordsWindowCanvasView.ContentRectTransform);
                recordContainer.Init(leaderboardEntry.Position, leaderboardEntry.DisplayName, leaderboardEntry.StatValue);
            }

            _loadingUIService.HideLoading();
        }
    }
}
