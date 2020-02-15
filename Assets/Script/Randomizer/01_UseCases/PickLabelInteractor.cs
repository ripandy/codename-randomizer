using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class PickLabelInteractor : BaseInteractor
    {
        public PickLabelInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.PickLabel) return;

            var labelId = requestMessage.Value;
            
            var randomizable = RandomizableGateway.GetActive();
            if (randomizable.HasLabel(labelId))
                randomizable.RemoveLabel(labelId);
            else
                randomizable.AddLabel(labelId);
            
            RespondPickLabel();
        }
    }
}