using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class CreateAccountCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _createAccountButton;

        [field: SerializeField] public TMP_InputField EmailInputField { get; private set; }
        [field: SerializeField] public TMP_InputField UsernameInputField { get; private set; }
        [field: SerializeField] public TMP_InputField PasswordInputField { get; private set; }

        private UnityAction _backAction;
        private UnityAction _createAccountAction;

        public void Init(UnityAction backAction, UnityAction createAccountAction)
        {
            _backAction = backAction;
            _createAccountAction = createAccountAction;

            _backButton.onClick.AddListener(_backAction);
            _createAccountButton.onClick.AddListener(_createAccountAction);
        }

        private void OnDestroy()
        {
            if (_backAction != null) _backButton.onClick.RemoveListener(_backAction);
            if (_createAccountAction != null) _createAccountButton.onClick.RemoveListener(_createAccountAction);
        }
    }
}
