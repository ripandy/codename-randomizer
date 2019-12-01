using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<ReloadResponseMessage> _reloadResponseInteractor;

        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private LoadSessionInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<ReloadResponseMessage> reloadResponseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _reloadResponseInteractor = reloadResponseInteractor;
        }
        
        private void Handle(int request)
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

            var response = new ReloadResponseMessage { Success = true, ItemNames = itemNames };
            _reloadResponseInteractor.OutputHandler.Invoke(response);
        }
        
        public class ReloadResponseInteractor : IOutputPortInteractor<ReloadResponseMessage>
        {
            public Action<ReloadResponseMessage> OutputHandler { get; set; }
        }
    }
}