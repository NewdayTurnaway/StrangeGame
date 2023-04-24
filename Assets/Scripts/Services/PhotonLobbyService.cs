using Photon.Pun;
using Photon.Realtime;
using Services;
using System;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Services
{
    public sealed class PhotonLobbyService : IDisposable, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        public const string CUSTOM_PROP_KEY = "CustomProperties";
        private const string PRIVATE = "Private";
        private const string PUBLIC = "Public";

        private readonly Updater _updater;
        private readonly ServerSettings _serverSettings;

        private readonly LoadBalancingClient _loadBalancingClient = new();
        private readonly TypedLobby _defaultLobby = new("CustomLobby", LobbyType.Default);

        public event Action<string> UpdateRoomInfoText = _ => { };
        public event Action CreatedRoom = () => { };
        public event Action JoinedLobby = () => { };
        public event Action<string, string, bool> JoinedRoom = (_, _, _) => { };
        public event Action<List<RoomInfo>> RoomListUpdate = (_) => { };

        public PhotonLobbyService(Updater updater, ServerSettings serverSettings)
        {
            _updater = updater;
            _serverSettings = serverSettings;

            _loadBalancingClient.AddCallbackTarget(this);
            _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);

            _updater.SubscribeToUpdate(OnUpdate);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(OnUpdate);
            _loadBalancingClient.Disconnect();
        }

        private void OnUpdate()
        {
            if (_loadBalancingClient == null)
            {
                return;
            }

            _loadBalancingClient.Service();

            if (_loadBalancingClient.CurrentRoom != null)
            {
                UpdateRoomInfoText.Invoke(_loadBalancingClient.CurrentRoom.ToStringFull());
            }
        }

        public void ConnectToSelectedRoom(string roomName)
        {
            var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = roomName };
            _loadBalancingClient.OpJoinRoom(enterRoomParams);
        }
        
        public void CreateRoom(string roomName, bool isPrivate)
        {
            string newRoomName;
            if (string.IsNullOrEmpty(roomName))
            {
                newRoomName = $"Game Room {Random.Range(0, 100)}";
            }
            else
            {
                newRoomName = roomName;
            }

            var isPublic = !isPrivate;
            var customProperty = isPublic ? PUBLIC : PRIVATE;

            var roomOptions = new RoomOptions
            {
                MaxPlayers = 4,
                CustomRoomProperties = new Hashtable { { CUSTOM_PROP_KEY, customProperty } },
                CustomRoomPropertiesForLobby = new[] { CUSTOM_PROP_KEY },
                IsVisible = isPublic,
                IsOpen = true,
                PublishUserId = true,
                PlayerTtl = 10000
            };

            var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = newRoomName, RoomOptions = roomOptions };
            _loadBalancingClient.OpJoinOrCreateRoom(enterRoomParams);
        }
        
        public void ConnectToPrivateRoom(string roomName)
        {
            string newRoomName;
            if (string.IsNullOrEmpty(roomName))
            {
                return;
            }
            else
            {
                newRoomName = roomName;
            }

            var enterRoomParams = new EnterRoomParams { Lobby = _defaultLobby, RoomName = newRoomName };
            _loadBalancingClient.OpJoinRoom(enterRoomParams);
        }

        public void LeaveRoom()
        {
            _loadBalancingClient.OpLeaveRoom(false);
        }

        public void OpenOrCloseRoom(Action openRoom, Action closeRoom)
        {
            _loadBalancingClient.CurrentRoom.IsOpen = !_loadBalancingClient.CurrentRoom.IsOpen;

            if (_loadBalancingClient.CurrentRoom.IsOpen)
            {
                openRoom();
            }
            else
            {
                closeRoom();
            }
        }

        public void OnConnected() { }

        public void OnConnectedToMaster()
        {
            Debug.Log(nameof(OnConnectedToMaster));
            _loadBalancingClient.OpJoinLobby(_defaultLobby);
        }

        public void OnDisconnected(DisconnectCause cause) { }

        public void OnRegionListReceived(RegionHandler regionHandler) { }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data) { }

        public void OnCustomAuthenticationFailed(string debugMessage) { }

        public void OnFriendListUpdate(List<FriendInfo> friendList) { }

        public void OnCreatedRoom() 
        {
            CreatedRoom.Invoke();
        }

        public void OnCreateRoomFailed(short returnCode, string message) { }

        public void OnJoinedRoom() 
        {
            var name = _loadBalancingClient.CurrentRoom.Name;
            var info = _loadBalancingClient.CurrentRoom.ToStringFull();
            var isOpen = _loadBalancingClient.CurrentRoom.IsOpen;
            JoinedRoom.Invoke(name, info, isOpen);
        }

        public void OnJoinRoomFailed(short returnCode, string message) { }

        public void OnJoinRandomFailed(short returnCode, string message) { }

        public void OnLeftRoom() { }

        public void OnJoinedLobby() 
        {
            JoinedLobby.Invoke();
        }

        public void OnLeftLobby() { }

        public void OnRoomListUpdate(List<RoomInfo> roomList) 
        {
            RoomListUpdate.Invoke(roomList);
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    }
}
