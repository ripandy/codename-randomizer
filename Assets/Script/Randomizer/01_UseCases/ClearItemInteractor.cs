using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ClearItemInteractor : IInputPortInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private ClearItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle()
        {
            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.Clear();
            _randomizableGateway.Save(id);

            _responseInteractor.RespondDisplayRandomizable(id);
        }
    }
}