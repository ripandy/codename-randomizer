using Randomizer.UseCases;
using UnityEngine;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ItemInputController : IInitializable
    {
        public bool IsInitialized { get; private set; }

        private readonly IInputPortInteractor<int, string> _editItemInteractor;
        private readonly IActionHandler<string> _editItemActionHandler;
        
        private readonly IInputPortInteractor<int> _removeItemInteractor;
        private readonly IActionHandler<int> _removeItemActionHandler;
        
        private readonly IOrderedView _itemView;
        
        private ItemInputController(
            IInputPortInteractor<int, string> editItemInteractor,
            IActionHandler<string> editItemActionHandler,
            IInputPortInteractor<int> removeItemInteractor,
            IActionHandler<int> removeItemActionHandler,
            IOrderedView itemView)
        {
            _editItemInteractor = editItemInteractor;
            _editItemActionHandler = editItemActionHandler;
            _removeItemInteractor = removeItemInteractor;
            _removeItemActionHandler = removeItemActionHandler;
            _itemView = itemView;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            _editItemActionHandler.OnAction = OnEditItem;
            _removeItemActionHandler.OnAction = _removeItemInteractor.InputHandler;
        }

        private void OnEditItem(string value)
        {
            var index = _itemView.Order;
            if (string.IsNullOrEmpty(value))
                _removeItemInteractor.InputHandler.Invoke(index);
            else
                _editItemInteractor.InputHandler.Invoke(index, value);
        }
    }
}