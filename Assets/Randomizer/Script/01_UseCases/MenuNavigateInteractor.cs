using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class MenuNavigateInteractor : BaseInteractor
    {
        public MenuNavigateInteractor(
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
            if (requestMessage.RequestType != RequestType.MenuNavigate) return;
            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            var index = LabelGateway.ActiveId >= 0 ? LabelGateway.GetIndex(LabelGateway.ActiveId) : -1;
            ResponseInteractor.Response(new LoadMenuResponseMessage(labels, index));
        }
    }
}