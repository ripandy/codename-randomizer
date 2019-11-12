using Randomizer.Entity;

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
            var randomizable = _randomizableGateway.GetById(_session.ActiveRandomizableId);
                randomizable.Clear();
                
            _clearItemOutputPortInteractor.Handle(new ReloadRandomizableResponseMessage {Success = true});
        }
    }
}