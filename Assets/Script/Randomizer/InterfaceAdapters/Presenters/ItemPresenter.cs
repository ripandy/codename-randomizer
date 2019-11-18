namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ItemPresenter : IPresenter<ItemViewModel>
    {
        public bool Visible { get; set; } = true;
        public ItemViewModel ViewModelObject { get; } = new ItemViewModel();
    }
}