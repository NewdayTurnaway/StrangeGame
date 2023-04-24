using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class ConnectToPrivateRoomWindowCanvasView : BaseMenuWindowCanvasView
    {
        [field: SerializeField] public TMP_InputField PrivateRoomNameInputField { get; private set; }
        [field: SerializeField] public Button ConnectButton { get; private set; }
    }
}
