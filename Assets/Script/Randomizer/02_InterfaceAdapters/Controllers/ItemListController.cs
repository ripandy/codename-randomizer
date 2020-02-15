using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class ItemListController : BaseController
    {
        private readonly IActionHandler<string> _editItemActionHandler;
        private readonly IActionHandler _removeItemActionHandler;
        private readonly IOrderedView _itemView;

        private ItemListController(
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IOrderedView itemView)
            : base(requestInteractor, actionHandlers)
        {
            _editItemActionHandler = ActionHandlers[0] as IActionHandler<string>;
            _removeItemActionHandler = ActionHandlers[1];
            _itemView = itemView;
        }
        
        protected override void BindAction()
        {       
            _editItemActionHandler.OnAction = () => RequestEditItem(_editItemActionHandler.Value);
            _removeItemActionHandler.OnAction = RequestRemoveItem;
        }

        private void RequestEditItem(string value)
        {
            var index = _itemView.Order;
            if (string.IsNullOrEmpty(value))
                RequestRemoveItem();
            else
                RequestInteractor.Request(new EditItemRequestMessage(index, value));
        }

        private void RequestRemoveItem()
        {
            RequestInteractor.Request(new RequestMessage<int>(RequestType.RemoveItem, _itemView.Order));
        }
    }
}