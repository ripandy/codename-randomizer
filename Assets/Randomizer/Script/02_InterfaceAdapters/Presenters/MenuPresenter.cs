using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class MenuPresenter : BasePresenter
    {
        private readonly IFactoryHandler<IItemView> _itemFactory;
        private readonly IList<IItemView> _itemViews = new List<IItemView>();
        private readonly IView _container;
        private readonly IView _deselectView;
        private readonly IView _highlightView;
        private readonly IOrderedView _bottom;

        private MenuPresenter(
            IResponseInteractor responseInteractor,
            IFactoryHandler<IItemView> itemFactory,
            IList<IView> views,
            IOrderedView bottom)
            : base(responseInteractor)
        {
            _itemFactory = itemFactory;
            _container = views[0];
            _deselectView = views[1];
            _highlightView = views[2];
            _bottom = bottom;
        }
        
        protected override void OnResponse(LoadMenuResponseMessage responseMessage)
        {
            UpdateContents(ItemType.MenuLabelList, responseMessage.Items);
            _container.Visible = true;
            _bottom.Order = responseMessage.ItemCount;
            _highlightView.Position = responseMessage.ActiveIndex >= 0 ? _itemViews[responseMessage.ActiveIndex].Position : _deselectView.Position;
        }
        
        protected override void PostResponse()
        {
            _container.Visible = DisplayState == DisplayState.DisplayMenu;
        }

        private void UpdateContents(ItemType itemType, IReadOnlyList<string> values)
        {
            var count = values.Count;
            for (var i = 0; i < count; i++)
            {
                if (i >= _itemViews.Count)
                    AddNextBatch(itemType);
                var item = _itemViews[i];
                    item.Text = values[i];
                    item.Order = i;
                    item.Visible = true;
            }

            for (var i = count; i < _itemViews.Count; i++)
            {
                _itemViews[i].Visible = false;
            }
        }
        
        private void AddNextBatch(ItemType itemType)
        {
            var index = _itemViews.Count;
            for (var i = 0; i < 4; i++)
            {
                var newItem = _itemFactory.Create((int) itemType);
                    newItem.Order = index + i;
                    newItem.Visible = false;
                _itemViews.Add(newItem);
            }
        }
    }
}