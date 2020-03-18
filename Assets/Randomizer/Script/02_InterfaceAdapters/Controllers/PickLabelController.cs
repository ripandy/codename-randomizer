using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class PickLabelController : BaseController
    {
        private readonly IOrderedView _labelView;
        
        public PickLabelController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            IOrderedView labelView)
            : base(requestInteractor, actionHandler)
        {
            _labelView = labelView;
        }

        protected override void BindAction()
        {
            ActionHandler.OnAction = RequestToggleLabel;
        }

        private void RequestToggleLabel()
        {
            RequestInteractor.Request(new RequestMessage<int>(RequestType.PickLabel, _labelView.Order));
        }
    }
}