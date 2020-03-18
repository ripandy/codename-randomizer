using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class MenuPresenter : BasePresenter
    {
        private readonly IFactoryHandler<IItemView> _itemFactory;
        private readonly IList<IItemView> _itemViews = new List<IItemView>();
        private readonly IView _container;
        private readonly IOrderedView _bottom;

        private MenuPresenter(
            IResponseInteractor responseInteractor,
            IFactoryHandler<IItemView> itemFactory,
            IView container,
            IOrderedView bottom)
            : base(responseInteractor)
        {
            _itemFactory = itemFactory;
            _container = container;
            _bottom = bottom;
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            _container.Visible = responseMessage.ResponseType == ResponseType.DisplayMenu;
            
            if (responseMessage.ResponseType != ResponseType.DisplayMenu) return;
            
            ClearItems();
            UpdateContents(ItemType.MenuLabelList, responseMessage.Items);
            _container.Visible = true;
            _bottom.Order = responseMessage.ItemCount;
        }
        
        private void UpdateContents(ItemType itemType, IReadOnlyList<string> values)
        {
            var count = values.Count;
            for (var i = 0; i < count; i++)
            {
                AddItem(itemType, values[i], i);
            }
        }
        
        private void AddItem(ItemType itemType, string itemName, int order)
        {
            var newItem = _itemFactory.Create((int) itemType);
            newItem.Text = itemName;
            newItem.Order = order;
            _itemViews.Add(newItem);
        }

        private void ClearItems()
        {
            foreach (var itemView in _itemViews)
            {
                itemView.Dispose();
            }
            _itemViews.Clear();
        }
    }
}