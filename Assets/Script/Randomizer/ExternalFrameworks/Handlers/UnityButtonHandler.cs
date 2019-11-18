using Randomizer.InterfaceAdapters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class UnityButtonHandler : MonoBehaviour
    {
        [SerializeField] protected Button button;

        [Inject] private IActionHandler _actionHandler;

        private void Start()
        {
            BindReactive();
        }

        protected virtual void BindReactive()
        {
            button.onClick.AddListener(() => _actionHandler.Handle());
        }
    }
}