using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadRandomizableInteractor : BaseInteractor
    {
        protected LoadRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
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