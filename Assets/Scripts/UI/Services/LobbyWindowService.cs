using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace UI.Services
{
    public sealed class LobbyWindowService : IDisposable
    {
        private readonly LoadingUIService _loadingUIService;
        private readonly PlayerDataService _playerDataService;
        private readonly MultiplayerView _multiplayerView;

        private string _selectedRoomName;

        public LobbyWindowService(
            LoadingUIService loadingUIService,
            PlayerDataService playerDataService,
            //Photon
            MainMenuCanvasView mainMenuCanvasView,
            MultiplayerView multiplayerView
            )
        {
            _loadingUIService = loadingUIService;
            _playerDataService = playerDataService;
            _multiplayerView = multiplayerView;

            if (!_playerDataService.IsLoginSuccess)
            {
                mainMenuCanvasView.MultiplayerButton.interactable = false;
                return;
            }

            InitLobbyWindow();
        }

        private void InitLobbyWindow()
        {
            _multiplayerView.LobbyWindowCanvasView.RefreshButton.onClick.AddListener(RefreshLobby);
            _multiplayerView.LobbyWindowCanvasView.CreateRoomButton.onClick.AddListener(OpenCreateRoom);
            _multiplayerView.LobbyWindowCanvasView.ConnectToPrivateRoomButton.onClick.AddListener(OpenConnectToPrivateRoom);
            _multiplayerView.LobbyWindowCanvasView.SelectedRoomName += OnSelectedRoomName;
            _multiplayerView.LobbyWindowCanvasView.ConnectToRoomButton.onClick.AddListener(ConnectToSelectedRoom);

            _multiplayerView.CreateRoomWindowCanvasView.CreateRoomButton.onClick.AddListener(CreateRoom);

            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ConnectButton.onClick.AddListener(ConnectToPrivateRoom);

            _multiplayerView.RoomWindowCanvasView.LeaveButton.onClick.AddListener(LeaveRoom);
            _multiplayerView.RoomWindowCanvasView.OpenOrCloseButton.onClick.AddListener(OpenOrCloseRoom);

            _multiplayerView.RoomWindowCanvasView.ShowCanvas(false);
            _multiplayerView.CreateRoomWindowCanvasView.ShowCanvas(false);
            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ShowCanvas(false);
        }

        public void Dispose()
        {
            _multiplayerView.LobbyWindowCanvasView.RefreshButton.onClick.RemoveListener(RefreshLobby);
            _multiplayerView.LobbyWindowCanvasView.CreateRoomButton.onClick.RemoveListener(OpenCreateRoom);
            _multiplayerView.LobbyWindowCanvasView.ConnectToPrivateRoomButton.onClick.RemoveListener(OpenConnectToPrivateRoom);
            _multiplayerView.LobbyWindowCanvasView.SelectedRoomName -= OnSelectedRoomName;
            _multiplayerView.LobbyWindowCanvasView.ConnectToRoomButton.onClick.RemoveListener(ConnectToSelectedRoom);

            _multiplayerView.CreateRoomWindowCanvasView.CreateRoomButton.onClick.RemoveListener(CreateRoom);

            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ConnectButton.onClick.RemoveListener(ConnectToPrivateRoom);

            _multiplayerView.RoomWindowCanvasView.LeaveButton.onClick.RemoveListener(LeaveRoom);
            _multiplayerView.RoomWindowCanvasView.OpenOrCloseButton.onClick.RemoveListener(OpenOrCloseRoom);
        }

        private void RefreshLobby()
        {
            _loadingUIService.ShowLoading();
            //
        }
        
        private void OpenCreateRoom()
        {
            _multiplayerView.CreateRoomWindowCanvasView.ShowCanvas(true);
        }
        
        private void OpenConnectToPrivateRoom()
        {
            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ShowCanvas(true);
        }
        
        private void OnSelectedRoomName(string roomName)
        {
            _selectedRoomName = roomName;
        }
        
        private void ConnectToSelectedRoom()
        {
            //Photon
            //var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = _selectedRoomName };
            //_loadBalancingClient.OpJoinRoom(enterRoomParams);
        }
        
        private void CreateRoom()
        {
            //Photon
            //string roomName;
            //if (string.IsNullOrEmpty(_createRoomWindow.RoomNameInputField.text))
            //{
            //    roomName = $"Game Room {Random.Range(0, 100)}";
            //}
            //else
            //{
            //    roomName = _createRoomWindow.RoomNameInputField.text;
            //}

            //var isPublic = !_createRoomWindow.IsPrivate.isOn;
            //var customProperty = isPublic ? PUBLIC : PRIVATE;

            //var roomOptions = new RoomOptions
            //{
            //    MaxPlayers = 4,
            //    CustomRoomProperties = new Hashtable { { CUSTOM_PROP_KEY, customProperty } },
            //    CustomRoomPropertiesForLobby = new[] { CUSTOM_PROP_KEY },
            //    IsVisible = isPublic,
            //    IsOpen = true,
            //    PublishUserId = true,
            //    PlayerTtl = 10000
            //};

            //var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = roomName, RoomOptions = roomOptions };
            //_loadBalancingClient.OpJoinOrCreateRoom(enterRoomParams);
        }
        
        private void ConnectToPrivateRoom()
        {
            //Photon
            //string roomName;
            //if (string.IsNullOrEmpty(_connectToPrivateRoomWindow.PrivateRoomNameInputField.text))
            //{
            //    return;
            //}
            //else
            //{
            //    roomName = _connectToPrivateRoomWindow.PrivateRoomNameInputField.text;
            //}

            //var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = roomName };
            //_loadBalancingClient.OpJoinRoom(enterRoomParams);
        }

        private void LeaveRoom()
        {
            _multiplayerView.RoomWindowCanvasView.ShowCanvas(false);
            //_loadBalancingClient.OpLeaveRoom(false);
        }

        private void OpenOrCloseRoom()
        {
            //_loadBalancingClient.CurrentRoom.IsOpen = !_loadBalancingClient.CurrentRoom.IsOpen;

            //if (_loadBalancingClient.CurrentRoom.IsOpen)
            //{
            //    _multiplayerView.RoomWindowCanvasView.OpenRoom();
            //}
            //else
            //{
            //    _multiplayerView.RoomWindowCanvasView.CloseRoom();
            //}
        }
    }
}
