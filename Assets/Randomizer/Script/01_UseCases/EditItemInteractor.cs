using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditItemInteractor : BaseInteractor
    {
        private EditItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(EditItemRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.EditItem) return;

            var itemId = RandomizableGateway.Active.ItemIds[requestMessage.ItemIndex];
            var item = ItemGateway.GetById(itemId);
                item.Name = requestMessage.NewItemName;
            ItemGateway.Save(itemId);
            
            RespondRandomizable(RandomizableGateway.Active);
        }
    }
}