using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class UnityOrderedButtonHandler : MonoBehaviour, IActionHandler<int>
    {
        [SerializeField] protected Button button;

        public Action<int> OnAction { get; set; }
        public int ActionParam { get; set; }
        
        Action IActionHandler.OnAction
        {
            set => OnAction = _ => OnAction.Invoke(ActionParam);
        }

        private void Start()
        {
            BindReactive();
        }

        protected virtual void BindReactive()
        {
            button.onClick.AddListener(() =>
            {
                if (gameObject.activeInHierarchy)
                    OnAction.Invoke(ActionParam);
            });
        }
    }
}