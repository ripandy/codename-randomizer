using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ClearItemInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private ClearItemInteractor(
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
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.Clear();
            _randomizableGateway.Save(id);

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.ClearValue();
            _responseInteractor.RaiseResponseEvent();
        }
    }
}