using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ItemController
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly IOrderedView _itemView;
        
        private ItemController(
            IRequestInteractor requestInteractor,
            IActionHandler<string> editItemActionHandler,
            IActionHandler removeItemActionHandler,
            IOrderedView itemView)
        {
            _requestInteractor = requestInteractor;
            _itemView = itemView;
            
            editItemActionHandler.OnAction = () => RequestEditItem(editItemActionHandler.Value);
            removeItemActionHandler.OnAction = RequestRemoveItem;
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
    }
}