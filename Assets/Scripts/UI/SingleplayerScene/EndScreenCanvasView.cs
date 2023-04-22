using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class EndScreenCanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private RectTransform _levelCompleteTransform;
        [SerializeField] private RectTransform _gameOverTransform;
        [SerializeField] private Button _nextLevelButton;

        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _exitButton;

        [field: SerializeField] public LevelStatsView LevelStatsView { get; private set; }

        private UnityAction _nextLevelAction;
        private UnityAction _quitAction;
        private UnityAction _exitAction;

        public bool IsEnabled => _canvas.enabled;

        private void OnValidate()
        {
            _canvas ??= GetComponent<Canvas>();
        }

        public void ShowLevelComplete()
        {
            _levelCompleteTransform.gameObject.SetActive(true);
            _gameOverTransform.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(true);
        }

        public void ShowGameOver()
        {
            _levelCompleteTransform.gameObject.SetActive(false);
            _gameOverTransform.gameObject.SetActive(true);
            _nextLevelButton.gameObject.SetActive(false);
        }

        public void Init(UnityAction nextLevelAction, UnityAction quitAction, UnityAction exitAction)
        {
            _levelCompleteTransform.gameObject.SetActive(false);
            _gameOverTransform.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);

            _nextLevelAction = nextLevelAction;
            _quitAction = quitAction;
            _exitAction = exitAction;

            _nextLevelButton.onClick.AddListener(_nextLevelAction);
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

        private void OnDestroy()
        {
            if (_nextLevelAction != null) _nextLevelButton.onClick.RemoveListener(_nextLevelAction);
            if (_quitAction != null) _quitButton.onClick.RemoveListener(_quitAction);
            if (_exitAction != null) _exitButton.onClick.RemoveListener(_exitAction);
        }
    }
}
