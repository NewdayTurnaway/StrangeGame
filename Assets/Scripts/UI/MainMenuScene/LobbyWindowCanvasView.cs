//using Photon.Realtime;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LobbyWindowCanvasView : BaseMenuWindowCanvasView
	{
        [SerializeField] private RoomContainerView _roomContainerView;

        private readonly List<RoomContainerView> _roomContainers = new();

        [field: SerializeField] public Button RefreshButton { get; private set; }
        [field: SerializeField] public Button CreateRoomButton { get; private set; }
        [field: SerializeField] public Button ConnectToPrivateRoomButton { get; private set; }
        [field: SerializeField] public Button ConnectToRoomButton { get; private set; }
        [field: SerializeField] public RectTransform ContentRectTransform { get; private set; }

        public event Action<string> SelectedRoomName = _ => { };

        private void OnDestroy()
        {
            foreach (var roomContainer in _roomContainers)
            {
                roomContainer.OnRoomSelected -= OnRoomSelected;
            }

            _roomContainers.Clear();
            ConnectToRoomButton.onClick.RemoveAllListeners();
        }

        //public void GetNewRooms(List<RoomInfo> roomInfos)
        //{
        //    foreach (RectTransform child in _contentRectTransform)
        //    {
        //        _roomContainers.Clear();
        //        Destroy(child.gameObject);
        //    }

        //    foreach (var roomInfo in roomInfos)
        //    {
        //        if (!roomInfo.IsVisible)
        //        {
        //            continue;
        //        }

        //        var room = Instantiate(_roomContainerView, _contentRectTransform);
        //        room.Init(roomInfo.Name, roomInfo.IsOpen, roomInfo.PlayerCount, roomInfo.MaxPlayers, roomInfo.CustomProperties.ToStringFull());
        //        room.OnRoomSelected += OnRoomSelected;
        //        _roomContainers.Add(room);
        //    }
        //}

        private void OnRoomSelected(string roomName)
        {
            SelectedRoomName.Invoke(roomName);
        }
    }
}
