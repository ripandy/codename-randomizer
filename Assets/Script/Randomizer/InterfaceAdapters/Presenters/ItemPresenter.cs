namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ItemPresenter : IPresenter<ItemViewModel>
    {
        public ItemViewModel ViewModelObject { get; } = new ItemViewModel();

        public bool Visible { get; set; }
    }
}