using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveItemInteractor : IInputPortInteractor<int>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;
        
        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private RemoveItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor
        )
        {
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(int request)
        {
            if (_randomizableGateway.ActiveId < 0) return;

            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.RemoveItem(request);
            _randomizableGateway.Save(id);
            
            _responseInteractor.RespondDisplayRandomizable(id);
        }
    }
}