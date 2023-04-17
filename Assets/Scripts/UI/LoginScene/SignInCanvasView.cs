using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class SignInCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _signInButton;

        [field: SerializeField] public TMP_InputField EmailInputField { get; private set; }
        [field: SerializeField] public TMP_InputField PasswordInputField { get; private set; }

        private UnityAction _backAction;
        private UnityAction _signInAction;

        public void Init(UnityAction backAction, UnityAction signInAction)
        {
            _backAction = backAction;
            _signInAction = signInAction;

            _backButton.onClick.AddListener(_backAction);
            _signInButton.onClick.AddListener(_signInAction);
        }

        public void SetInputs(string email, string password)
        {
            EmailInputField.text = email;
            PasswordInputField.text = password;
        }

        private void OnDestroy()
        {
            if (_backAction != null) _backButton.onClick.RemoveListener(_backAction);
            if (_signInAction != null) _signInButton.onClick.RemoveListener(_signInAction);
        }
    }
}
