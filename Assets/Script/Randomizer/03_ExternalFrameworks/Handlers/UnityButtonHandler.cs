using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class UnityButtonHandler : MonoBehaviour, IActionHandler
    {
        [SerializeField] protected Button button;

        public Action OnAction { private get; set; }
        public bool Active
        {
            get => button.enabled;
            set => button.enabled = value;
        }

        private void Start()
        {
            BindReactive();
            Active = false;
            Active = true;
        }

        protected virtual void BindReactive()
        {
            button.onClick.AddListener(() =>
            {
                if (gameObject.activeInHierarchy)
                    OnAction.Invoke();
            });
        }
    }
}