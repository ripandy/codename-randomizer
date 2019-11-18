using System.Collections.Generic;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizablePresenter
    {
        private readonly IFactoryHandler<IPresenter> _itemFactory;
        private readonly IList<ItemPresenter> _itemPresenters = new List<ItemPresenter>();
        public int ItemCount { get; private set; }

        private RandomizablePresenter(IFactoryHandler<IPresenter> itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public void AddItem(string itemName, int order)
        {
            if (_itemPresenters.Count <= ItemCount)
            {
                var newItem = _itemFactory.Create() as ItemPresenter;
                _itemPresenters.Add(newItem);
            }

            var vm = _itemPresenters[ItemCount].ViewModelObject;
                vm.Order = order;
                vm.Text = itemName;

            ItemCount++;
        }

        public void Clear()
        {
            HideAllItems();
            ItemCount = 0;
        }

        public void ShowAllItems(bool show = true)
        {
            for (var i = 0; i < _itemPresenters.Count; i++)
            {
                var itemPresenter = _itemPresenters[i];
                itemPresenter.Visible = show && i < ItemCount;
            }
        }
        public void HideAllItems() => ShowAllItems(false);
    }
}