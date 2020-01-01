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
        [SerializeField] private bool showPlaceholderWhenActiveAndEmpty;

        private EventSystem _eventSystem;
        private string _value;

        public Action<string> OnAction { get; set; }

        Action IActionHandler.OnAction
        {
            set { OnAction = _ => OnAction.Invoke(_value); }
        }

        protected virtual void Awake()
        {
            _eventSystem = EventSystem.current;
        }

        private void Start()
        {
            BindReactive();
        }

        protected virtual void BindReactive()
        {
            inputField.onSubmit.AddListener(OnSubmit);
            inputField.onSelect.AddListener(OnSelect);
        }

        private void OnSubmit(string value)
        {
            _value = value;
            OnAction.Invoke(value);
            inputField.text = "";
            ShowPlaceholder(true);
            _eventSystem.SetSelectedGameObject(null);
        }

        private void OnSelect(string value)
        {
            ShowPlaceholder(showPlaceholderWhenActiveAndEmpty && string.IsNullOrEmpty(value));
        }
        
        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }
    }
}