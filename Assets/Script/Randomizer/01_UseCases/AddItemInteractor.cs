using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : IInputPortInteractor<string>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        private AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(string request)
        {
            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
            var newItem = new Item { Name = request };
            randomizable.AddItem(newItem);
            _randomizableGateway.Save(id);
            
            _responseInteractor.RespondDisplayRandomizable(id);
        }
    }
}