using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditItemInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        
        private EditItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor) 
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(EditItemRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.EditItem) return;
            
            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
            randomizable.Items[requestMessage.ItemId].Name = requestMessage.NewItemName;
            _randomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}