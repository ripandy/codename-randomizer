using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : BaseInteractor
    {
        protected AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddItem || string.IsNullOrEmpty(requestMessage.Value)) return;

            var newItem = new Item(requestMessage.Value);
            ItemGateway.AddNew(newItem);

            var id = RandomizableGateway.ActiveId;
            var randomizable = RandomizableGateway.GetById(id);
                randomizable.AddItem(newItem.Id);
            RandomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}