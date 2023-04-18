using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class BaseMenuWindowCanvasView : BaseCanvasView
    {
        [SerializeField] private Button _backButton;

        private void GoBack()
        {
            ShowCanvas(false);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(GoBack);
        }

        private void Start()
        {
            _backButton.onClick.AddListener(GoBack);
        }
    }
}