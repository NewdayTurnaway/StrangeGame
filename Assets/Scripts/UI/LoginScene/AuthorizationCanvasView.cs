using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class AuthorizationCanvasView : BaseCanvasView
	{
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _createAccountButton;

        private UnityAction _exitAction;
        private UnityAction _signInAction;
        private UnityAction _createAccountAction;

        public void Init(UnityAction exitAction, UnityAction signInAction, UnityAction createAccountAction)
        {
            _exitAction = exitAction;
            _signInAction = signInAction;
            _createAccountAction = createAccountAction;

            _exitButton.onClick.AddListener(_exitAction);
            _signInButton.onClick.AddListener(_signInAction);
            _createAccountButton.onClick.AddListener(_createAccountAction);
        }

        private void OnDestroy()
        {
            if (_exitAction != null) _exitButton.onClick.RemoveListener(_exitAction);
            if (_signInAction != null) _signInButton.onClick.RemoveListener(_signInAction);
            if (_createAccountAction != null) _createAccountButton.onClick.RemoveListener(_createAccountAction);
        }
    }
}
