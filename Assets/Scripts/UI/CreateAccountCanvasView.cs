using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class CreateAccountCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _backButton;
        [field: SerializeField] public Button CreateAccountButton { get; private set; }

        [field: SerializeField] public TMP_InputField EmailInputField { get; private set; }
        [field: SerializeField] public TMP_InputField UsernameInputField { get; private set; }
        [field: SerializeField] public TMP_InputField PasswordInputField { get; private set; }



        private UnityAction _backAction;

        public void Init(UnityAction backAction)
        {
            _backAction = backAction;
            _backButton.onClick.AddListener(_backAction);
        }

        private void OnDestroy()
        {
            if (_backAction != null) _backButton.onClick.RemoveListener(_backAction);
        }
    }
}
