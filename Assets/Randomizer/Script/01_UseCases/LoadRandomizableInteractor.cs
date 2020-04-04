using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadRandomizableInteractor : BaseInteractor
    {
        protected LoadRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(LoadRandomizableRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.LoadRandomizable) return;
            if (!requestMessage.LoadActive)
                RandomizableGateway.ActiveId = RandomizableGateway[requestMessage.RandomizableIndex].Id;
            RespondRandomizable(RandomizableGateway.Active);
        }
    }
}