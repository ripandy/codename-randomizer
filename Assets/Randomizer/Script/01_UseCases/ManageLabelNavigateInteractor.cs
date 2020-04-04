using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ManageLabelNavigateInteractor : BaseInteractor
    {
        public ManageLabelNavigateInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.ManageLabelNavigate) return;
            RespondManageLabel();
        }
    }
}