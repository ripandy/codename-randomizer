using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class UnityButtonHandler : MonoBehaviour, IActionHandler
    {
        [SerializeField] protected Button button;

        public Action OnAction { get; set; }

        private void Start()
        {
            BindReactive();
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