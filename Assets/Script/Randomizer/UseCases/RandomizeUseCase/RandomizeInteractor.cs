using Randomizer.Entity;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _resultPresenter;

        private RandomizeInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<RandomizeResponseMessage> resultPresenter)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _resultPresenter = resultPresenter;
        }
        
        public void Handle()
        {
            var randomizable = _randomizableGateway.GetById(_session.ActiveRandomizableId);
            var item = randomizable.Randomize();
            var response = new RandomizeResponseMessage { Success = true, PickedItemName = item.Name };
                
            _resultPresenter.Handle(response);
        }
    }
}