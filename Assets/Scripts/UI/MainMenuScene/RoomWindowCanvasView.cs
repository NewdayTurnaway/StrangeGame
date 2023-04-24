using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class RoomWindowCanvasView : BaseCanvasView
    {
        private const string OPEN = "Open";
        private const string CLOSE = "Close";

        [SerializeField] private TMP_Text _roomLableText;
        [SerializeField] private TMP_Text _roomInfoText;
        [SerializeField] private Color _openColor;
        [SerializeField] private Color _closeColor;

        [field: SerializeField] public Button LeaveButton { get; private set; }
        [field: SerializeField] public Button OpenOrCloseButton { get; private set; }
        [field: SerializeField] public Button ReadyButton { get; private set; }

        public void InitRoom(string roomName, string roomInfo, bool isOpen)
        {
            _roomLableText.text = roomName;
            _roomInfoText.text = roomInfo;

            if (isOpen)
            {
                OpenRoom();
            }
            else
            {
                CloseRoom();
            }
        }

        public void UpdateRoomInfoText(string roomInfo)
        {
            _roomInfoText.text = roomInfo;
        }

        public void OpenRoom()
        {
            var buttonText = OpenOrCloseButton.GetComponentInChildren<TMP_Text>();
            buttonText.color = _openColor;
            buttonText.text = OPEN;
        }

        public void CloseRoom()
        {
            var buttonText = OpenOrCloseButton.GetComponentInChildren<TMP_Text>();
            buttonText.color = _closeColor;
            buttonText.text = CLOSE;
        }
    }
}
