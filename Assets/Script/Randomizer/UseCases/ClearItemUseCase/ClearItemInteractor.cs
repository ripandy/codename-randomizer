using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ClearItemInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<ReloadRandomizableResponseMessage> _clearItemOutputPortInteractor;

        private ClearItemInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<ReloadRandomizableResponseMessage> clearItemOutputPortInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _clearItemOutputPortInteractor = clearItemOutputPortInteractor;
        }
        
        public void Handle()
        {
            var id = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.Clear();
            _randomizableGateway.Save(id);
                
            _clearItemOutputPortInteractor.Handle(new ReloadRandomizableResponseMessage {Success = true});
        }
    }
}