using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemResponseHandler : IOutputPortInteractor<AddItemResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;
        
        private AddItemResponseHandler(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
        }
        
        public void Handle(AddItemResponseMessage response)
        {
            if (!response.Success) return; // TODO : show response message?

            _randomizablePresenter.AddItem(response.NewItemName, response.NewItemOrder);
            _addItemPresenter.ViewModelObject.Order = _randomizablePresenter.ItemCount;
        }
    }
}