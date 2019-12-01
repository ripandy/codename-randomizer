using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ClearItemInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<ReloadResponseMessage> _reloadResponseInteractor;

        public Action InputHandler => Handle;

        private ClearItemInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<ReloadResponseMessage> reloadResponseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _reloadResponseInteractor = reloadResponseInteractor;
        }
        
        private void Handle()
        {
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.Clear();
            _randomizableGateway.Save(id);

            var response = new ReloadResponseMessage {Success = true, Title = randomizable.Name, ItemNames = new string[0]};
            _reloadResponseInteractor.OutputHandler.Invoke(response);
        }
    }
}