using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Services
{
    public sealed class LoadingUIService
    {
        private readonly LoadingCanvasView _loadingCanvas;

        private bool _isLoading;

        public LoadingUIService(LoadingCanvasView loadingCanvas)
        {
            _loadingCanvas = loadingCanvas;
            _loadingCanvas.ShowCanvas(false);
        }

        public void ShowLoading()
        {
            if (!_isLoading)
            {
                _loadingCanvas.ShowCanvas(true);
                _isLoading = true;
                LoadingProgressAsync();
            }
        }

        public void HideLoading()
        {
            if (_isLoading)
            {
                _loadingCanvas.ShowCanvas(false);
                _isLoading = false;
            }
        }

        private async void LoadingProgressAsync()
        {
            while (_isLoading)
            {
                _loadingCanvas.LoadingSlider.value = 
                    _loadingCanvas.LoadingSlider.value == _loadingCanvas.LoadingSlider.maxValue ? 
                    _loadingCanvas.LoadingSlider.minValue : 
                    _loadingCanvas.LoadingSlider.value + Time.deltaTime;
                await Task.Yield();
            }
        }
    }
}