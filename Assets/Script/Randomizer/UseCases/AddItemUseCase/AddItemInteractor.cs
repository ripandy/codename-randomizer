using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<string>
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

        public void Handle(string request)
        {
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
            var newItem = new Item { Name = request };
            var order = randomizable.ItemCount;
            randomizable.AddItem(newItem);
            _randomizableGateway.Save(id);

            var responseMessage = new AddItemResponseMessage { Success = true, NewItemOrder = order, NewItemName = newItem.Name };
            _addItemOutputPortInteractor.Handle(responseMessage);
        }
    }
}