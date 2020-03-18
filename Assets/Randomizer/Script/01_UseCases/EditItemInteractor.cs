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
            
            var id = requestMessage.ItemId;
            var item = ItemGateway.GetById(id);
                item.Name = requestMessage.NewItemName;
            ItemGateway.Save(id);
            
            RespondRandomizable(RandomizableGateway.Active);
        }
    }
}