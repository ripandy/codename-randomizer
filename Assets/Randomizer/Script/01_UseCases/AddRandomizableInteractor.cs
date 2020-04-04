using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : BaseInteractor
    {
        private AddRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddRandomizable) return;
            
            var randomizable = new Randomizable();
            
            if (LabelGateway.ActiveId >= 0)
                randomizable.AddLabel(LabelGateway.ActiveId);
            
            RandomizableGateway.AddNew(randomizable);
            RandomizableGateway.ActiveId = randomizable.Id;
            
            RespondRandomizable(randomizable);
        }
    }
}