using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : IInputPortInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private AddRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            var randomizable = new Randomizable();
            var newId = _randomizableGateway.AddNew(randomizable);
            _randomizableGateway.ActiveId = newId;

            _responseInteractor.RespondDisplayRandomizable(newId);
        }
    }
}