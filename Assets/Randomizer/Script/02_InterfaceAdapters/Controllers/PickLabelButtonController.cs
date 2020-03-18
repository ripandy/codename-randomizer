using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class PickLabelButtonController : BaseController
    {
        public PickLabelButtonController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler)
            : base(requestInteractor, actionHandler)
        {
        }

        protected override void BindAction()
        {
            ActionHandler.OnAction = RequestNavigatePickLabel;
        }

        private void RequestNavigatePickLabel()
        {
            RequestInteractor.Request(new RequestMessage(RequestType.PickLabelNavigate));
        }
    }
}