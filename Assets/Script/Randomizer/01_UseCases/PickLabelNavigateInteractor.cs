using System.Linq;
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

            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            var randomizable = RandomizableGateway.GetActive();
            var labelIds = randomizable.LabelIds;
            var pickedLabelIds = labelIds;
            ResponseInteractor.Response(new PickLabelListResponseMessage(randomizable.Name, labels, pickedLabelIds));
        }
    }
}