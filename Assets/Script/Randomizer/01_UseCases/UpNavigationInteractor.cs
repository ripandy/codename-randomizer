using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class UpNavigationInteractor : IInputPortInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IGateway<Label> _labelGateway;
        private readonly IResponseInteractor _responseInteractor;
        
        public Action InputHandler => Handle;

        private UpNavigationInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IResponseInteractor responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
            _labelGateway = labelGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            var currentState = _responseInteractor.ResponseType;
            if (_randomizableGateway.ActiveId >= 0 && currentState == ResponseType.DisplayResult)
                _responseInteractor.RespondDisplayRandomizable(_randomizableGateway.ActiveId);
            else
                RespondShowLabel();
        }

        private void RespondShowLabel()
        {
            if (_randomizableGateway.ActiveId >= 0)
            {
                var id = _randomizableGateway.ActiveId;
                var randomizable = _randomizableGateway.GetById(id);
                if (string.IsNullOrEmpty(randomizable.Name) && randomizable.ItemCount == 0)
                    _randomizableGateway.Remove(id);
            }
            
            _randomizableGateway.ActiveId = -1;
            _responseInteractor.RespondDisplayLabel(_labelGateway.ActiveId);
        }
    }
}