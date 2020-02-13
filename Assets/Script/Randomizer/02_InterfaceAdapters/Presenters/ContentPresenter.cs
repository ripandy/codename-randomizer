using System.Collections.Generic;
using System.Linq;
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
            if (DisplayState == DisplayState.DisplayMenu) return;
            
            ClearItems();
            ClearSubItems();
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            switch (responseMessage.ResponseType)
            {
                case ResponseType.DisplayLabel:
                    UpdateContents(ItemType.Randomizable, responseMessage.Items);
                    break;
                case ResponseType.DisplayResult:
                    UpdateContents(ItemType.Result, responseMessage.Items);
                    break;
            }
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            UpdateContents(ItemType.Item, responseMessage.Items);
            UpdateSubContents(ItemType.PickLabelButton, responseMessage.Labels);
        }
        
        protected override void OnResponse(PickLabelListResponseMessage responseMessage)
        {
            UpdateContents(ItemType.PickLabelList, responseMessage.Items);
            UpdateToggles(responseMessage.PickedLabels);
        }

        protected override void PostResponse()
        {
            if (DisplayState == DisplayState.DisplayMenu) return;
            
            var containerType = ContainerType.Vertical;
            if (DisplayState == DisplayState.DisplayLabel)
                containerType = ContainerType.Grid;
            
            _viewContainer.Type = containerType;
        }

        private void UpdateContents(ItemType itemType, IReadOnlyList<string> values)
        {
            var count = values.Count;
            for (var i = 0; i < count; i++)
            {
                AddItem(itemType, values[i], i);
            }
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

        private void UpdateToggles(IReadOnlyList<int> active)
        {
            for (var i = 0; i < _itemViews.Count; i++)
            {
                var toggleable = (IPickLabelView) _itemViews[i];
                    toggleable.Toggle = active.Contains(i);
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