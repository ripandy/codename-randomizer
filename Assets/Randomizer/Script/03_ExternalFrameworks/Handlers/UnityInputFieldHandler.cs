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
        
        private string _prevValue;

        public string Value { get; private set; }
        public Action OnAction { get; set; }
        public bool Active
        {
            get => inputField.interactable;
            set => inputField.interactable = value;
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
                var text = placeholderText.text;
                inputField.text = text;
                _prevValue = text;
            }
            
            ShowPlaceholder(false);
        }
        
        private void OnDeselect(string value)
        {
            Value = value;
            InvokeAction();
            inputField.text = "";
            ShowPlaceholder(true);
        }

        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }

        private void InvokeAction()
        {
            if (Value.Equals(_prevValue)) return;
            
            OnAction.Invoke();
            _prevValue = Value;
        }
    }
}