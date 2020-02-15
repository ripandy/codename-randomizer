using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class PickLabelNavigateInteractor : BaseInteractor
    {
        public PickLabelNavigateInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.PickLabelNavigate) return;
            RespondPickLabel();
        }
    }
}