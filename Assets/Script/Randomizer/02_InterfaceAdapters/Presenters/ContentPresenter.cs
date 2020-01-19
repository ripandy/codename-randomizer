using System.Collections.Generic;
using Randomizer.UseCases;
using UnityEngine;

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

        protected override void OnResponse()
        {
            base.OnResponse();
            var prevType = _viewContainer.Type;
            _viewContainer.Type = ResponseInteractor.ResponseType == ResponseType.DisplayLabel ? ContentType.Grid : ContentType.Vertical;
            if (prevType != _viewContainer.Type)
                ClearItems();
            UpdateContents();
        }

        private void UpdateContents()
        {
            var values = ResponseInteractor.Values;
            var count = ResponseInteractor.ValueCount;
            for (var i = 0; i < count; i++)
            {
                if (i < _itemViews.Count)
                {
                    var item = _itemViews[i];
                        item.Text = values[i];
                        item.Order = i;
                }
                else
                    AddItem(values[i], i);
            }

            for (var i = _itemViews.Count - 1; i >= count; i--)
            {
                RemoveItem(i);
            }
        }

        private void AddItem(string itemName, int order)
        {
            var newItem = _itemFactory.Create((int) _viewContainer.Type);
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

        private void RemoveItem(int index)
        {
            _itemViews[index].Dispose();
            _itemViews.RemoveAt(index);
        }

        private void ShowAllItems(bool show = true)
        {
            foreach (var item in _itemViews)
            {
                item.Visible = show;
            }
        }
    }
}