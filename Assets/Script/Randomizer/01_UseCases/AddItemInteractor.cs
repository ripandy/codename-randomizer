using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<string>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        private AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(string request)
        {
            var randomizable = _randomizableGateway.GetActive();
            var newItem = new Item { Name = request };
            randomizable.AddItem(newItem);
            _randomizableGateway.SaveActive();

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.AddValue(newItem.Name);
            _responseInteractor.RaiseResponseEvent();
        }
    }
}