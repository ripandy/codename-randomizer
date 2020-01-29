using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ContentPresenter : BasePresenter
    {
        private readonly IFactoryHandler<IItemView> _itemFactory;
        private readonly IList<IItemView> _itemViews = new List<IItemView>();
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

        protected override void OnResponse(LabelResponseMessage responseMessage)
        {
            UpdateContents(ContainerType.Grid, responseMessage.Items);
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            UpdateContents(ContainerType.Vertical, responseMessage.Items);
        }

        protected override void OnResponse(ResultResponseMessage responseMessage)
        {
            UpdateContents(ContainerType.Vertical, responseMessage.Items);
        }

        private void UpdateContents(ContainerType newType, IReadOnlyList<string> values)
        {
            ClearItems();
            
            var count = values.Count;
            for (var i = 0; i < count; i++)
            {
                AddItem(values[i], i);
            }

            _viewContainer.Type = newType;
        }

        private void AddItem(string itemName, int order)
        {
            var itemType = DisplayState == DisplayState.DisplayLabel ? ItemType.Randomizable : ItemType.Item;
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