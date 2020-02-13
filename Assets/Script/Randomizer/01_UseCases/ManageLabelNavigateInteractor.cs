using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class ManageLabelNavigateInteractor : BaseInteractor
    {
        public ManageLabelNavigateInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.ManageLabelNavigate) return;
            
            // TODO : respond manage label
        }
    }
}