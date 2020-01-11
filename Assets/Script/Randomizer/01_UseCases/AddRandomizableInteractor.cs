using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : IInputPortInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private AddRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            var randomizable = new Randomizable();
            var newId = _randomizableGateway.AddNew(randomizable);
            _randomizableGateway.ActiveId = newId;

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.ClearValue();
            _responseInteractor.RaiseResponseEvent();
        }
    }
}