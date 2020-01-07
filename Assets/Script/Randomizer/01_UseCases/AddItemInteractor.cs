using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<string>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        private AddItemInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(string request)
        {
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
            var newItem = new Item { Name = request };
            randomizable.AddItem(newItem);
            _randomizableGateway.Save(id);

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.AddValue(newItem.Name);
            _responseInteractor.RaiseResponseEvent();
        }
    }
}