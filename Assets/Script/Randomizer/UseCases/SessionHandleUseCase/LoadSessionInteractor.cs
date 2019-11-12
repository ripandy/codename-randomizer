using System.Collections.Generic;
using Randomizer.Entity;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<LoadSessionRequestMessage>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<ReloadRandomizableResponseMessage> _loadSessionOutputPortInteractor;

        private LoadSessionInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<ReloadRandomizableResponseMessage> loadSessionOutputPortInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _loadSessionOutputPortInteractor = loadSessionOutputPortInteractor;
        }
        
        public void Handle(LoadSessionRequestMessage request)
        {
            var id = request.CurrentSessionId;
            _session.ActiveRandomizableId = id;

            var randomizable = _randomizableGateway.GetById(id);
            var itemNames = new List<string>();
            foreach (var item in randomizable.Items)
            {
                itemNames.Add(item.Name);
            }

            var responseMessage = new ReloadRandomizableResponseMessage { Success = true, ItemNames = itemNames.ToArray() };
            _loadSessionOutputPortInteractor.Handle(responseMessage);
        }
    }
}