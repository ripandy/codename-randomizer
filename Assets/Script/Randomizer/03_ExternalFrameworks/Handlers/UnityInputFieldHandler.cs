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
        [SerializeField] private bool clearOnDeselect;
        [SerializeField] private bool showPlaceholderWhenEmptyAndActive;

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
            Debug.Log($"OnSubmit value : {value}, Event System? {_eventSystem == null}");
            if (inputField.isFocused)
                _eventSystem.SetSelectedGameObject(null);
        }

        private void OnSelect(string value)
        {
            Debug.Log($"OnSelect value : {value}");
            var isEmpty = string.IsNullOrEmpty(value);
            var show = isEmpty && showPlaceholderWhenEmptyAndActive;
            ShowPlaceholder(show);
        }
        
        private void OnDeselect(string value)
        {
            Debug.Log($"OnDeselect value : {value}");
            _value = value;
            InvokeAction();
            if (clearOnDeselect)
                inputField.text = "";
            var isEmpty = string.IsNullOrEmpty(inputField.text);
            ShowPlaceholder(isEmpty);
        }

        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }

        private void InvokeAction()
        {
            if (_value.Equals(_prevValue)) return;
            
            Debug.Log($"Invoking value : {_value}");
            OnAction.Invoke(_value);
            _prevValue = _value;
        }
    }
}