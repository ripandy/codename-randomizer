using Randomizer.UseCases;
using UnityEngine;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ItemInputController : IInitializable
    {
        public bool IsInitialized { get; private set; }

        private readonly IRequestInteractor _editItemInteractor;
        private readonly IActionHandler<string> _editItemActionHandler;
        
        private readonly IRequestInteractor _removeItemInteractor;
        private readonly IActionHandler<int> _removeItemActionHandler;
        
        private readonly IOrderedView _itemView;
        
        private ItemInputController(
            IRequestInteractor editItemInteractor,
            IActionHandler<string> editItemActionHandler,
            IRequestInteractor removeItemInteractor,
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
            _removeItemActionHandler.OnAction = _removeItemInteractor.HandleRequest;
        }

        private void OnEditItem(string value)
        {
            var index = _itemView.Order;
            if (string.IsNullOrEmpty(value))
                _removeItemInteractor.HandleRequest.Invoke(index);
            else
                _editItemInteractor.HandleRequest.Invoke(index, value);
        }
    }
}