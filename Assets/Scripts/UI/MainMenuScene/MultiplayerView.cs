using UnityEngine;

namespace UI
{
    public sealed class MultiplayerView : MonoBehaviour
	{
        [field: SerializeField] public LobbyWindowCanvasView LobbyWindowCanvasView { get; private set; }
        [field: SerializeField] public CreateRoomWindowCanvasView CreateRoomWindowCanvasView { get; private set; }
        [field: SerializeField] public ConnectToPrivateRoomWindowCanvasView ConnectToPrivateRoomWindowCanvasView { get; private set; }
        [field: SerializeField] public RoomWindowCanvasView RoomWindowCanvasView { get; private set; }
    }
}
