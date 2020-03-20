using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class PickLabelInteractor : BaseInteractor
    {
        public PickLabelInteractor(
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
            if (requestMessage.RequestType != RequestType.PickLabel) return;

            var labelId = LabelGateway[requestMessage.Value].Id;
            
            var randomizable = RandomizableGateway.Active;
            if (randomizable.HasLabel(labelId))
                randomizable.RemoveLabel(labelId);
            else
                randomizable.AddLabel(labelId);
            
            RespondPickLabel();
        }
    }
}