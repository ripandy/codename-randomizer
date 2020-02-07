using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : BaseInteractor
    {
        protected AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddItem || string.IsNullOrEmpty(requestMessage.Value)) return;

            var id = RandomizableGateway.ActiveId;
            var randomizable = RandomizableGateway.GetById(id);
            var newItem = new Item { Name = requestMessage.Value };
            randomizable.AddItem(newItem);
            RandomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}