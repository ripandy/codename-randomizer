using System.Collections.Generic;

namespace Randomizer.InterfaceAdaptor.Presenter
{
    public class RandomizablePresenter
    {
        private readonly IList<ItemPresenter> _itemPresenters = new List<ItemPresenter>();

        public void AddPresenter(ItemPresenter newItem) => _itemPresenters.Add(newItem);

        public void ShowAllItems(bool show = true)
        {
            foreach (var itemPresenter in _itemPresenters)
            {
                itemPresenter.Visible = show;
            }
        }

        public void HideAllItems() => ShowAllItems(false);
    }
}