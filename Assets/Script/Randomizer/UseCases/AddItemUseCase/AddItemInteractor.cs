using Randomizer.Entity;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<AddItemRequestMessage>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<AddItemResponseMessage> _addItemOutputPortInteractor;

        private AddItemInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<AddItemResponseMessage> addItemOutputPortInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _addItemOutputPortInteractor = addItemOutputPortInteractor;
        }

        public void Handle(AddItemRequestMessage request)
        {
            var randomizable = _randomizableGateway.GetById(_session.ActiveRandomizableId);
            var newItem = new Item { Name = request.ItemName };
            var order = randomizable.ItemCount;
            randomizable.AddItem(newItem);

            var responseMessage = new AddItemResponseMessage { Success = true, NewItemOrder = order, NewItemName = newItem.Name };
            _addItemOutputPortInteractor.Handle(responseMessage);
        }
    }
}