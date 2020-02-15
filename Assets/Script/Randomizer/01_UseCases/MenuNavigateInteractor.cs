using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class MenuNavigateInteractor : BaseInteractor
    {
        public MenuNavigateInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.MenuNavigate) return;
            var labels = LabelGateway.GetAll().Select(label => label.Name).ToArray();
            ResponseInteractor.Response(new ItemListResponseMessage(ResponseType.DisplayMenu, "", labels));
        }
    }
}