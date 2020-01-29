using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ItemController : IInitializable
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly IActionHandler<string> _editItemActionHandler;
        private readonly IActionHandler _removeItemActionHandler;
        private readonly IOrderedView _itemView;
        
        private ItemController(
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IOrderedView itemView)
        {
            _requestInteractor = requestInteractor;
            _editItemActionHandler = actionHandlers[0] as IActionHandler<string>;
            _removeItemActionHandler = actionHandlers[1];
            _itemView = itemView;
        }

        private void RequestEditItem(string value)
        {
            var index = _itemView.Order;
            if (string.IsNullOrEmpty(value))
                RequestRemoveItem();
            else
                _requestInteractor.Request(new EditItemRequestMessage(index, value));
        }

        private void RequestRemoveItem()
        {
            _requestInteractor.Request(new RequestMessage<int>(RequestType.RemoveItem, _itemView.Order));
        }

        public bool IsInitialized { get; private set; }
        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
                
            _editItemActionHandler.OnAction = () => RequestEditItem(_editItemActionHandler.Value);
            _removeItemActionHandler.OnAction = RequestRemoveItem;
        }
    }
}