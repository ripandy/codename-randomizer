using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemPresenter :
        IOutputPortInteractor<AddItemResponseMessage>,
        IPresenter<AddItemViewModel>
    {
        public bool Visible { get; set; } = true;
        public AddItemViewModel ViewModelObject { get; } = new AddItemViewModel();
        
        private readonly RandomizablePresenter _randomizablePresenter;

        private AddItemPresenter(
            RandomizablePresenter randomizablePresenter)
        {
            _randomizablePresenter = randomizablePresenter;
        }

        public void Handle(AddItemResponseMessage response)
        {
            if (!response.Success) return; // TODO : show response message?

            _randomizablePresenter.AddItem(response.NewItemName, response.NewItemOrder);

            ViewModelObject.Order = _randomizablePresenter.ItemCount;
        }
    }
}