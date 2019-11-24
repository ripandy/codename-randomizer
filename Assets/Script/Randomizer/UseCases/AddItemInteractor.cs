using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<string>, IOutputPortInteractor<AddItemResponseMessage>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        public Action<AddItemResponseMessage> OutputHandler { get; set; }

        private AddItemInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
        }

        private void Handle(string request)
        {
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
            var newItem = new Item { Name = request };
            var order = randomizable.ItemCount;
            randomizable.AddItem(newItem);
            _randomizableGateway.Save(id);

            var responseMessage = new AddItemResponseMessage { Success = true, NewItemOrder = order, NewItemName = newItem.Name };
            OutputHandler.Invoke(responseMessage);
        }
    }
}