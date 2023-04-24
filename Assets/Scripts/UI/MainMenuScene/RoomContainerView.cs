using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class RoomContainerView : MonoBehaviour
    {
        [SerializeField] private Button _roomButton;
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private TMP_Text _isOpenText;
        [SerializeField] private TMP_Text _playerCountText;
        [SerializeField] private TMP_Text _customPropertiesText;

        private string _roomName;

        public event Action<string> OnRoomSelected = _ => { };

        private void OnValidate()
        {
            _roomButton ??= GetComponent<Button>();
        }

        private void Start()
        {
            _roomButton.onClick.AddListener(SelectRoom);
        }

        private void OnDestroy()
        {
            _roomButton.onClick.RemoveListener(SelectRoom);
        }

        private void SelectRoom()
        {
            OnRoomSelected.Invoke(_roomName);
        }

        public void Init(string name, bool isOpen, int playerCount, int maxPlayers, string customProperties)
        {
            _roomName = name;
            _roomNameText.text = name;
            _isOpenText.text = isOpen.ToString();
            _playerCountText.text = $"{playerCount} / {maxPlayers}";
            _customPropertiesText.text = customProperties;

            _roomButton.interactable = isOpen;
        }
    }
}
