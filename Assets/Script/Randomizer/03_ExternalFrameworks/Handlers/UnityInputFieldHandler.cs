using System;
using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class UnityInputFieldHandler : MonoBehaviour, IActionHandler<string>
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text placeholderText;
        [SerializeField] private bool copyPlaceholder;

        private EventSystem _eventSystem;
        
        private string _value;
        private string _prevValue;

        public Action<string> OnAction { get; set; }

        Action IActionHandler.OnAction
        {
            set { OnAction = _ => OnAction.Invoke(_value); }
        }

        private void Start()
        {
            _eventSystem = EventSystem.current;
            BindReactive();
        }

        protected virtual void BindReactive()
        {
            inputField.onSubmit.AddListener(OnSubmit);
            inputField.onSelect.AddListener(OnSelect);
            inputField.onDeselect.AddListener(OnDeselect);
        }

        private void OnSubmit(string value)
        {
            if (inputField.isFocused)
                _eventSystem.SetSelectedGameObject(null);
        }

        private void OnSelect(string value)
        {
            if (copyPlaceholder)
            {
                inputField.text = placeholderText.text;
                ShowPlaceholder(false);
            }
            else
            {
                var isEmpty = string.IsNullOrEmpty(value);
                ShowPlaceholder(isEmpty);
            }
        }
        
        private void OnDeselect(string value)
        {
            _value = value;
            InvokeAction();
            inputField.text = "";
            ShowPlaceholder(false);
        }

        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }

        private void InvokeAction()
        {
            if (_value.Equals(_prevValue)) return;
            
            OnAction.Invoke(_value);
            _prevValue = _value;
        }
    }
}