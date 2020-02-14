using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddLabelInteractor : BaseInteractor
    {
        protected AddLabelInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddLabel || string.IsNullOrEmpty(requestMessage.Value)) return;

            var newLabel = new Label { Name = requestMessage.Value };
            var newId = LabelGateway.AddNew(newLabel);
            LabelGateway.ActiveId = newId;
            
            RespondManageLabel();
        }
    }
}