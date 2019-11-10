using Randomizer.Entity;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor<RandomizeRequestMessage>
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor<RandomizeResponseMessage> _resultPresenter;

        private RandomizeInteractor(
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor<RandomizeResponseMessage> resultPresenter)
        {
            _randomizableGateway = randomizableGateway;
            _resultPresenter = resultPresenter;
        }
        
        public void Handle(RandomizeRequestMessage request)
        {
            var randomizable = _randomizableGateway.GetById(request.RandomizableId);
            var item = randomizable.Randomize();
            var response = new RandomizeResponseMessage { Success = true, PickedItemName = item.Name };
                
            _resultPresenter.Handle(response);
        }
    }
}