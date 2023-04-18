using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _singleplayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _recordsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _signOutButton;
        [SerializeField] private Button _exitButton;

        private UnityAction _singleplayerAction;
        private UnityAction _multiplayerAction;
        private UnityAction _recordsAction;
        private UnityAction _settingsAction;
        private UnityAction _signOutAction;
        private UnityAction _exitAction;

        public void Init(
            UnityAction singleplayerAction, 
            UnityAction multiplayerAction,
            UnityAction recordsAction,
            UnityAction settingsAction,
            UnityAction signOutAction,
            UnityAction exitAction)
        {
            _singleplayerAction = singleplayerAction;
            _multiplayerAction = multiplayerAction;
            _recordsAction = recordsAction;
            _settingsAction = settingsAction;
            _signOutAction = signOutAction;
            _exitAction = exitAction;

            _singleplayerButton.onClick.AddListener(_singleplayerAction);
            _multiplayerButton.onClick.AddListener(_multiplayerAction);
            _recordsButton.onClick.AddListener(_recordsAction);
            _settingsButton.onClick.AddListener(_settingsAction);
            _signOutButton.onClick.AddListener(_signOutAction);
            _exitButton.onClick.AddListener(_exitAction);
        }

        private void OnDestroy()
        {
            if (_singleplayerAction != null) _singleplayerButton.onClick.RemoveListener(_singleplayerAction);
            if (_multiplayerAction != null) _multiplayerButton.onClick.RemoveListener(_multiplayerAction);
            if (_recordsAction != null) _recordsButton.onClick.RemoveListener(_recordsAction);
            if (_settingsAction != null) _settingsButton.onClick.RemoveListener(_settingsAction);
            if (_signOutAction != null) _signOutButton.onClick.RemoveListener(_settingsAction);
            if (_exitAction != null) _exitButton.onClick.RemoveListener(_settingsAction);
        }
    }
}
