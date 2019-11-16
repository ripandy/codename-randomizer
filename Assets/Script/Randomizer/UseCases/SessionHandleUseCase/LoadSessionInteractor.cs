using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
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
        
        public void Handle(int request)
        {
            _session.ActiveRandomizableId = request;

            var randomizable = _randomizableGateway.GetById(request);
            var itemCount = randomizable.ItemCount;
            var itemNames = new string[itemCount];
            for (var i = 0; i < itemCount; i++)
            {
                var item = randomizable.Items[i];
                itemNames[i] = (item.Name);
            }

            var responseMessage = new ReloadRandomizableResponseMessage { Success = true, ItemNames = itemNames };
            _loadSessionOutputPortInteractor.Handle(responseMessage);
        }
    }
}