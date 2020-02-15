using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditItemInteractor : BaseInteractor
    {
        private EditItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor) 
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(EditItemRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.EditItem) return;
            
            var id = RandomizableGateway.ActiveId;
            var randomizable = RandomizableGateway.GetById(id);
            randomizable.Items[requestMessage.ItemId].Name = requestMessage.NewItemName;
            RandomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}