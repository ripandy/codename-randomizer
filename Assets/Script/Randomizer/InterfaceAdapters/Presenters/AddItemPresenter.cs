using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemPresenter : IPresenter<AddItemViewModel>
    {
        public bool Visible { get; set; } = true;
        public AddItemViewModel ViewModelObject { get; } = new AddItemViewModel();
    }
}