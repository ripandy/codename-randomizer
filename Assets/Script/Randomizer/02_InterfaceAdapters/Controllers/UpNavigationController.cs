using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class UpNavigationController : BaseController
    {
        private readonly BasePresenter _presenter;

        private UpNavigationController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            BasePresenter presenter)
            : base(requestInteractor, actionHandler)
        {
            _presenter = presenter;
        }

        private void RequestUpNavigation()
        {
            switch (_presenter.DisplayState)
            {
                case DisplayState.DisplayRandomizable:
                case DisplayState.DisplayResults:
                case DisplayState.DisplayManageLabel:
                    RequestInteractor.Request(LoadLabelRequestMessage.RequestActive);
                    break;
                case DisplayState.DisplayResult:
                case DisplayState.DisplayPickLabel:
                    RequestInteractor.Request(LoadRandomizableRequestMessage.RequestActive);
                    break;
            }
        }

        protected override void BindAction()
        {
            ActionHandler.OnAction = RequestUpNavigation;
        }
    }
}