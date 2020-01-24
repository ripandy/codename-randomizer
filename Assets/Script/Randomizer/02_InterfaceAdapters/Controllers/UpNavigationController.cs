using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class UpNavigationController
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly BasePresenter _presenter;
        
        private UpNavigationController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            BasePresenter presenter)
        {
            _requestInteractor = requestInteractor;
            actionHandler.OnAction = RequestUpNavigation;
            _presenter = presenter;
        }

        private void RequestUpNavigation()
        {
            switch (_presenter.DisplayState)
            {
                case DisplayState.DisplayRandomizable:
                case DisplayState.DisplayResults:
                    _requestInteractor.Request(LoadLabelRequestMessage.RequestActive);
                    break;
                case DisplayState.DisplayResult:
                    _requestInteractor.Request(LoadRandomizableRequestMessage.RequestActive);
                    break;
            }
        }
    }
}