using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class AuthorizationCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _createAccountButton;

        private UnityAction _signInAction;
        private UnityAction _createAccountAction;

        public void Init(UnityAction signInAction, UnityAction createAccountAction)
        {
            _signInAction = signInAction;
            _createAccountAction = createAccountAction;

            _signInButton.onClick.AddListener(_signInAction);
            _createAccountButton.onClick.AddListener(_createAccountAction);
        }

        private void OnDestroy()
        {
            if (_signInAction != null) _signInButton.onClick.RemoveListener(_signInAction);
            if (_createAccountAction != null) _createAccountButton.onClick.RemoveListener(_createAccountAction);
        }
    }
}
