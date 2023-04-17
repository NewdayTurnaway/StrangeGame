using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class SignInCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _backButton;
        [field: SerializeField] public Button SignInButton { get; private set; }

        [field: SerializeField] public TMP_InputField EmailInputField { get; private set; }
        [field: SerializeField] public TMP_InputField PasswordInputField { get; private set; }

        private UnityAction _backAction;

        public void Init(UnityAction backAction)
        {
            _backAction = backAction;
            _backButton.onClick.AddListener(_backAction);
        }

        public void SetInputs(string email, string password)
        {
            EmailInputField.text = email;
            PasswordInputField.text = password;

        }

        private void OnDestroy()
        {
            if (_backAction != null) _backButton.onClick.RemoveListener(_backAction);
        }
    }
}
