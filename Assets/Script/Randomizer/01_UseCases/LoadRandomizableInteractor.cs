using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadRandomizableInteractor : BaseInteractor
    {
        protected readonly IGateway<Randomizable> RandomizableGateway;

        protected LoadRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            RandomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(LoadRandomizableRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.LoadRandomizable) return;
            if (!requestMessage.LoadActive)
                RandomizableGateway.ActiveId = requestMessage.RandomizableId;
            RespondRandomizable(RandomizableGateway.GetActive());
        }
    }
}