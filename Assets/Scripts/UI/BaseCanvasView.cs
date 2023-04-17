using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class BaseCanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public void ShowCanvas(bool isShown)
        {
            _canvas.enabled = isShown;
        }

        private void OnValidate()
        {
            _canvas ??= GetComponent<Canvas>();
        }
    }
}