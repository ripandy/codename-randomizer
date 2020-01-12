using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private LoadSessionInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle(int request)
        {
            _randomizableGateway.ActiveId = request;
            
            if (request == -1)
            {
                _responseInteractor.RespondDisplayLabel(_labelGateway.ActiveId);
            }
            else
            {
                _responseInteractor.RespondDisplayRandomizable(_randomizableGateway.ActiveId);
            }
        }
    }
}