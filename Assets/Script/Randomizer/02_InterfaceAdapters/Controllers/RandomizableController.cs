using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RandomizableController
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly IOrderedView _orderedView;
        
        private RandomizableController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            IOrderedView orderedView)
        {
            _requestInteractor = requestInteractor;
            _orderedView = orderedView;
            
            actionHandler.OnAction = RequestRandomizable;
        }
        
        private void RequestRandomizable()
        {
            _requestInteractor.Request(new LoadRandomizableRequestMessage(_orderedView.Order));
        }
    }
}