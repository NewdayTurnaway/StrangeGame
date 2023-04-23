using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public sealed class InputFieldsControl : MonoBehaviour
	{
		[SerializeField] private List<TMP_InputField> _inputFields;

		private EventSystem _eventSystem;
		private TMP_InputField _selectedInputField;

		private void Awake()
		{
            _eventSystem = EventSystem.current;
        }

		private void Update()
		{
			var currentSelectedGameObject = _eventSystem.currentSelectedGameObject;
			if (currentSelectedGameObject == null) return;

			if (currentSelectedGameObject.TryGetComponent(out TMP_InputField inputField))
			{
				_selectedInputField = inputField;
			}

			if(_selectedInputField == null) return;

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (_inputFields.Contains(_selectedInputField))
				{
                    var index =_inputFields.IndexOf(_selectedInputField);
                    index = (index + 1) % _inputFields.Count;
					_inputFields[index].Select();
                }
			}
		}
	}

}