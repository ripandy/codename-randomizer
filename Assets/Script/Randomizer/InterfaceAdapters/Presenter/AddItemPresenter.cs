using Randomizer.InterfaceAdapters;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdaptor.Presenter
{
    public class AddItemPresenter : IOutputPortInteractor<AddItemResponseMessage>
    {
        private readonly IFactoryHandler<IPresenter<ItemViewModel>> _itemFactory;
        private readonly RandomizablePresenter _randomizablePresenter;

        private AddItemPresenter(
            IFactoryHandler<ItemPresenter> itemFactory,
            RandomizablePresenter randomizablePresenter
            )
        {
            _itemFactory = itemFactory;
            _randomizablePresenter = randomizablePresenter;
        }

        public void Handle(AddItemResponseMessage response)
        {
            if (!response.Success) return; // TODO : show response message?
            
            var newItem = _itemFactory.Create() as ItemPresenter;
            if (newItem == null) return;
                newItem.Visible = true;
                
            var vm = newItem.ViewModelObject;
                vm.Order = response.NewItemOrder;
                vm.Text = response.NewItemName;
                
            _randomizablePresenter.AddPresenter(newItem);
        }
    }
}