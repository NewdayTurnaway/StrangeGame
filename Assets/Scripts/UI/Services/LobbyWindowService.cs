using Photon.Realtime;
using Services;
using System;
using System.Collections.Generic;

namespace UI.Services
{
    public sealed class LobbyWindowService : IDisposable
    {
        private readonly LoadingUIService _loadingUIService;
        private readonly PlayerDataService _playerDataService;
        private readonly PhotonLobbyService _photonLobbyService;
        private readonly MultiplayerView _multiplayerView;

        private string _selectedRoomName;

        public LobbyWindowService(
            LoadingUIService loadingUIService,
            PlayerDataService playerDataService,
            PhotonLobbyService photonLobbyService,
            MainMenuCanvasView mainMenuCanvasView,
            MultiplayerView multiplayerView
            )
        {
            _loadingUIService = loadingUIService;
            _playerDataService = playerDataService;
            _photonLobbyService = photonLobbyService;
            _multiplayerView = multiplayerView;

            //if (!_playerDataService.IsLoginSuccess)
            //{
            //    mainMenuCanvasView.MultiplayerButton.interactable = false;
            //    return;
            //}
            
            InitLobbyWindow();
            
            _photonLobbyService.UpdateRoomInfoText += OnUpdateRoomInfoText;
            _photonLobbyService.CreatedRoom += OnCreatedRoom;
            _photonLobbyService.JoinedLobby += OnJoinedLobby;
            _photonLobbyService.JoinedRoom += OnJoinedRoom;
            _photonLobbyService.RoomListUpdate += OnRoomListUpdate;
        }

        private void InitLobbyWindow()
        {
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
            _photonLobbyService.UpdateRoomInfoText -= OnUpdateRoomInfoText;
            _photonLobbyService.CreatedRoom -= OnCreatedRoom;
            _photonLobbyService.JoinedLobby -= OnJoinedLobby;
            _photonLobbyService.JoinedRoom -= OnJoinedRoom;
            _photonLobbyService.RoomListUpdate -= OnRoomListUpdate;

            _multiplayerView.LobbyWindowCanvasView.CreateRoomButton.onClick.RemoveListener(OpenCreateRoom);
            _multiplayerView.LobbyWindowCanvasView.ConnectToPrivateRoomButton.onClick.RemoveListener(OpenConnectToPrivateRoom);
            _multiplayerView.LobbyWindowCanvasView.SelectedRoomName -= OnSelectedRoomName;
            _multiplayerView.LobbyWindowCanvasView.ConnectToRoomButton.onClick.RemoveListener(ConnectToSelectedRoom);

            _multiplayerView.CreateRoomWindowCanvasView.CreateRoomButton.onClick.RemoveListener(CreateRoom);

            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ConnectButton.onClick.RemoveListener(ConnectToPrivateRoom);

            _multiplayerView.RoomWindowCanvasView.LeaveButton.onClick.RemoveListener(LeaveRoom);
            _multiplayerView.RoomWindowCanvasView.OpenOrCloseButton.onClick.RemoveListener(OpenOrCloseRoom);
        }

        private void OnUpdateRoomInfoText(string roomInfo)
        {
            _multiplayerView.RoomWindowCanvasView.UpdateRoomInfoText(roomInfo);
        }

        private void OnCreatedRoom()
        {
            _multiplayerView.CreateRoomWindowCanvasView.ShowCanvas(false);
            _multiplayerView.ConnectToPrivateRoomWindowCanvasView.ShowCanvas(false);


        }

        private void OnJoinedLobby()
        {
            _loadingUIService.HideLoading();
        }

        private void OnJoinedRoom(string name, string info, bool isOpen)
        {
            _multiplayerView.RoomWindowCanvasView.InitRoom(name, info, isOpen);
            _multiplayerView.RoomWindowCanvasView.ShowCanvas(true);
        }

        private void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _multiplayerView.LobbyWindowCanvasView.GetNewRooms(roomList);
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
            _photonLobbyService.ConnectToSelectedRoom(_selectedRoomName);
        }
        
        private void CreateRoom()
        {
            _photonLobbyService.CreateRoom(_multiplayerView.CreateRoomWindowCanvasView.RoomNameInputField.text, _multiplayerView.CreateRoomWindowCanvasView.IsPrivate.isOn);
        }
        
        private void ConnectToPrivateRoom()
        {
            _photonLobbyService.ConnectToPrivateRoom(_multiplayerView.ConnectToPrivateRoomWindowCanvasView.PrivateRoomNameInputField.text);
        }

        private void LeaveRoom()
        {
            _multiplayerView.RoomWindowCanvasView.ShowCanvas(false);
            _photonLobbyService.LeaveRoom();
        }

        private void OpenOrCloseRoom()
        {
            _photonLobbyService.OpenOrCloseRoom(_multiplayerView.RoomWindowCanvasView.OpenRoom, _multiplayerView.RoomWindowCanvasView.CloseRoom);
        }
    }
}
