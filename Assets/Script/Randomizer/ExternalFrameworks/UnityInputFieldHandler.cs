using System;
using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class UnityInputFieldHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        [Inject] private IActionHandler<string> _actionHandler;
        private EventSystem _eventSystem;

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
            _actionHandler.Handle(value);
            inputField.text = "";
            ShowPlaceholder(true);
            _eventSystem.SetSelectedGameObject(null);
        }

        private void OnSelect(string value)
        {
            Debug.Log("on select..");
            ShowPlaceholder(false);
        }
        
        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }
    }
}