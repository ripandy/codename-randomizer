using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddLabelInteractor : BaseInteractor
    {
        protected AddLabelInteractor(
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
            if (requestMessage.RequestType != RequestType.AddLabel || string.IsNullOrEmpty(requestMessage.Value)) return;

            var newLabel = new Label(requestMessage.Value);
            
            LabelGateway.AddNew(newLabel);
            LabelGateway.ActiveId = newLabel.Id;
            
            RespondManageLabel();
        }
    }
}