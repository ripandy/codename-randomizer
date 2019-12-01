using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizablePresenter : BasePresenter
    {
        private const string DefaultTitle = "Randomizer";

        private readonly IFactoryHandler<IItemView> _itemFactory;
        private readonly IList<IItemView> _itemViews = new List<IItemView>();
        private readonly ITextView _title;

        private RandomizablePresenter(
            IFactoryHandler<IItemView> itemFactory,
            IList<IOutputPortInteractor> responses,
            ITextView title)
        : base(responses)
        {
            _itemFactory = itemFactory;
            _title = title;
        }
        
        protected override void OnAddItem(AddItemResponseMessage responseMessage)
        {
            if (!responseMessage.Success) return;
            
            AddItem(responseMessage.NewItemName, responseMessage.NewItemOrder);
        }

        protected override void OnRandomize(RandomizeResponseMessage responseMessage)
        {
            ShowAllItems(!responseMessage.Success);
        }

        protected override void OnReload(ReloadResponseMessage responseMessage)
        {
            if (!responseMessage.Success) return;

            ClearItems();

            var title = responseMessage.Title;
            if (string.IsNullOrEmpty(title))
                title = DefaultTitle;
            _title.Text = title;
            
            var items = responseMessage.ItemNames;
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                AddItem(item, i);
            }
        }

        private void AddItem(string itemName, int order)
        {
            var newItem = _itemFactory.Create();
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

        private void RemoveItem(int order)
        {
            _itemViews[order].Dispose();
            _itemViews.RemoveAt(order);
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