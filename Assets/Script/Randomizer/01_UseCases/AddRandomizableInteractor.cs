using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : IInputPortInteractor<string>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<string> InputHandler => Handle;
        private const string Data = "";
        Action IInputPortInteractor.InputHandler => () => InputHandler(Data);

        private AddRandomizableInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle(string request)
        {
            var randomizable = new Randomizable { Name = request };
            _session.ActiveRandomizableId = _randomizableGateway.AddNew(randomizable);

            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            _responseInteractor.ClearValue();
            _responseInteractor.RaiseResponseEvent();
        }
    }
}