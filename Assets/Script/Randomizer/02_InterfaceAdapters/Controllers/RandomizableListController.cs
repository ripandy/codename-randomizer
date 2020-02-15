using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RandomizableListController : BaseController
    {
        private readonly IOrderedView _orderedView;

        private RandomizableListController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            IOrderedView orderedView)
            : base(requestInteractor, actionHandler)
        {
            _orderedView = orderedView;
        }

        protected override void BindAction()
        {
            ActionHandler.OnAction = RequestRandomizable;
        }
        
        private void RequestRandomizable()
        {
            RequestInteractor.Request(new LoadRandomizableRequestMessage(_orderedView.Order));
        }
    }
}