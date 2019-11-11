using Randomizer.InterfaceAdapters;

namespace Randomizer.InterfaceAdaptor.Presenter
{
    public class ItemPresenter : IPresenter<ItemViewModel>
    {
        public ItemViewModel ViewModelObject { get; }

        public bool Visible { get; set; }

        private ItemPresenter(ItemViewModel viewModelObject)
        {
            ViewModelObject = viewModelObject;
        }
    }
}