using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveItemInteractor : BaseInteractor
    {
        private RemoveItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            var id = RandomizableGateway.ActiveId;
            if (requestMessage.RequestType != RequestType.RemoveItem || id < 0) return;
            
            var randomizable = RandomizableGateway.GetById(id);
                randomizable.RemoveItem(requestMessage.Value);
            RandomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}