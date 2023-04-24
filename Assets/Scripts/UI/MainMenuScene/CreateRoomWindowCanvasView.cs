using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class CreateRoomWindowCanvasView : BaseMenuWindowCanvasView
    {
        [field: SerializeField] public TMP_InputField RoomNameInputField { get; private set; }
        [field: SerializeField] public Toggle IsPrivate { get; private set; }
        [field: SerializeField] public Button CreateRoomButton { get; private set; }
    }
}
