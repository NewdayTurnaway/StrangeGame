using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class PauseCanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _exitButton;
        
        //TODO SCORE

        private UnityAction _settingsAction;
        private UnityAction _quitAction;
        private UnityAction _exitAction;

        public bool IsEnabled => _canvas.enabled;

        private void OnValidate()
        {
            _canvas ??= GetComponent<Canvas>();
        }

        private void Start()
        {
            _backButton.onClick.AddListener(GoBack);
        }

        public void Init(UnityAction settingsAction, UnityAction quitAction, UnityAction exitAction)
        {
            _settingsAction = settingsAction;
            _quitAction = quitAction;
            _exitAction = exitAction;

            _settingsButton.onClick.AddListener(_settingsAction);
            _quitButton.onClick.AddListener(_quitAction);
            _exitButton.onClick.AddListener(_exitAction);
        }

        public void ShowCanvas(bool isShown)
        {
            _canvas.enabled = isShown;
            ChangeCursorLockSate(IsEnabled);
        }

        private void ChangeCursorLockSate(bool state)
        {
            if (state)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void GoBack()
        {
            ShowCanvas(false);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(GoBack);

            if (_settingsAction != null) _settingsButton.onClick.RemoveListener(_settingsAction);
            if (_quitAction != null) _quitButton.onClick.RemoveListener(_quitAction);
            if (_exitAction != null) _exitButton.onClick.RemoveListener(_exitAction);
        }
    }
}
