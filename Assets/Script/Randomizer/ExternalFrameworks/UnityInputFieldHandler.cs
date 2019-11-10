using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using TMPro;
using UnityEngine;

namespace Randomizer.ExternalFrameworks
{
    public class UnityInputFieldHandler : MonoBehaviour, IActionHandler<string>
    {
        [SerializeField] private TMP_InputField inputField;

        private IList<Action<string>> _subscribers = new List<Action<string>>();
        private bool _initialized;

        private void Start()
        {
            BindReactive();
            _initialized = true;
        }

        private void BindReactive()
        {
            foreach (var action in _subscribers)
            {
                inputField.onSubmit.AddListener(action.Invoke);
            }
            inputField.onSubmit.AddListener(_ =>
            {
                ShowPlaceholder(true);
            });
            inputField.onSelect.AddListener(_ =>
            {
                ShowPlaceholder(false);
            });

        }

        private void ShowPlaceholder(bool show)
        {
            inputField.placeholder.gameObject.SetActive(show);
        }

        public void Subscribe(Action<string> onAction)
        {
            if (_initialized)
            {
                inputField.onSubmit.AddListener(onAction.Invoke);
                Debug.Log("Subscription happened after initialization!!");
            }
            _subscribers.Add(onAction);
        }
    }
}