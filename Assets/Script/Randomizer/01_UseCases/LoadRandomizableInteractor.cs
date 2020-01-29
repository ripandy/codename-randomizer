using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadRandomizableInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;

        protected LoadRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(LoadRandomizableRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.LoadRandomizable) return;
            if (!requestMessage.LoadActive)
                _randomizableGateway.ActiveId = requestMessage.RandomizableId;
            RespondRandomizable(_randomizableGateway.GetActive());
        }
    }
}