using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ResetInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;
        
        public Action InputHandler => Handle;

        private ResetInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            var randomizableId = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(randomizableId);
                
            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.ClearValue();
            foreach (var item in randomizable.Items)
            {
                _responseInteractor.AddValue(item.Name);
            }
            _responseInteractor.RaiseResponseEvent();
        }
    }
}