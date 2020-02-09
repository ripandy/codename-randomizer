using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ContentPresenter : BasePresenter
    {
        private readonly IFactoryHandler<IItemView> _itemFactory;
        private readonly IList<IItemView> _itemViews = new List<IItemView>();
        private readonly IList<IItemView> _subItemViews = new List<IItemView>();
        private readonly IViewContainer _viewContainer;

        private ContentPresenter(
            IResponseInteractor responseInteractor,
            IFactoryHandler<IItemView> itemFactory,
            IViewContainer viewContainer)
            : base(responseInteractor)
        {
            _itemFactory = itemFactory;
            _viewContainer = viewContainer;
        }

        protected override void PreResponse()
        {
            ClearItems();
            ClearSubItems();
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            var containerType = ContainerType.Vertical;
            var itemType = ItemType.Item;
            switch (responseMessage.ResponseType)
            {
                case ResponseType.DisplayLabel:
                    containerType = ContainerType.Grid;
                    itemType = ItemType.Randomizable;
                    break;
                case ResponseType.DisplayResult:
                    itemType = ItemType.Result;
                    break;
            }
            UpdateContents(containerType, itemType, responseMessage.Items);
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            UpdateContents(ContainerType.Vertical, ItemType.Item, responseMessage.Items);
            UpdateSubContents(ItemType.PickLabelButton, responseMessage.Labels);
        }
        
        protected override void OnResponse(PickLabelListResponseMessage responseMessage)
        {
            UpdateContents(ContainerType.Vertical, ItemType.PickLabelList, responseMessage.Items);
        }

        private void UpdateContents(ContainerType containerType, ItemType itemType, IReadOnlyList<string> values)
        {
            var count = values.Count;
            for (var i = 0; i < count; i++)
            {
                AddItem(itemType, values[i], i);
            }

            _viewContainer.Type = containerType;
        }

        private void UpdateSubContents(ItemType itemType, IReadOnlyList<string> values)
        {
            var count = values.Count;
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    AddSubItem(itemType, values[i], i);
                }
            }
            else
            {
                AddSubItem(itemType, "", 0);
            }
        }

        private void AddItem(ItemType itemType, string itemName, int order)
        {
            var newItem = _itemFactory.Create((int) itemType);
                newItem.Text = itemName;
                newItem.Order = order;
            _itemViews.Add(newItem);
        }
        
        private void AddSubItem(ItemType itemType, string itemName, int order)
        {
            var newItem = _itemFactory.Create((int) itemType);
                newItem.Text = itemName;
                newItem.Order = order;
            _subItemViews.Add(newItem);
        }

        private void ClearItems()
        {
            foreach (var itemView in _itemViews)
            {
                itemView.Dispose();
            }
            _itemViews.Clear();
        }

        private void ClearSubItems()
        {
            foreach (var itemView in _subItemViews)
            {
                itemView.Dispose();
            }
            _subItemViews.Clear();
        }
    }
}