using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class ErrorCanvasView : BaseCanvasView
	{
        [SerializeField] private TMP_Text _errorMessage;

        public void SetErrorMessage(string errorMessage)
        {
            _errorMessage.text = errorMessage;
        }
    }
}
