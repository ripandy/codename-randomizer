using Randomizer.Entity;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<AddItemRequestMessage>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<AddItemResponseMessage> _addItemPresenter;

        private AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<AddItemResponseMessage> addItemPresenter)
        {
            _randomizableGateway = randomizableGateway;
            _addItemPresenter = addItemPresenter;
        }

        public void Handle(AddItemRequestMessage request)
        {
            var randomizable = _randomizableGateway.GetById(request.RandomizableId);
            var newItem = new Item { Name = request.ItemName };
            var order = randomizable.ItemCount;
            randomizable.AddItem(newItem);

            var responseMessage = new AddItemResponseMessage { Success = true, NewItemOrder = order, NewItemName = newItem.Name };
            _addItemPresenter.Handle(responseMessage);
        }
    }
}