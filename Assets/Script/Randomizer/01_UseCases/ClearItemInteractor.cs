using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ClearItemInteractor : IInputPortInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private ClearItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle()
        {
            var randomizable = _randomizableGateway.GetActive();
                randomizable.Clear();
            _randomizableGateway.SaveActive();

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.ClearValue();
            _responseInteractor.RaiseResponseEvent();
        }
    }
}