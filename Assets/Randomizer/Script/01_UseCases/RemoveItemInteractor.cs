using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveItemInteractor : BaseInteractor
    {
        private RemoveItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            var id = RandomizableGateway.ActiveId;
            if (requestMessage.RequestType != RequestType.RemoveItem || id < 0) return;

            var itemId = ItemGateway[requestMessage.Value].Id;
            var randomizable = RandomizableGateway.GetById(id);
                randomizable.RemoveItem(itemId);
            RandomizableGateway.Save(id);
            if (!RandomizableGateway.GetAll().Any(r => randomizable.HasItem(itemId)))
                ItemGateway.Remove(itemId);
            
            RespondRandomizable(randomizable);
        }
    }
}