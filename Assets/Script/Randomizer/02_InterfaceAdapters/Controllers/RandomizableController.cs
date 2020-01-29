using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RandomizableController : IInitializable
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly IActionHandler _actionHandler;
        private readonly IOrderedView _orderedView;
        
        private RandomizableController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            IOrderedView orderedView)
        {
            _requestInteractor = requestInteractor;
            _actionHandler = actionHandler;
            _orderedView = orderedView;
        }
        
        private void RequestRandomizable()
        {
            _requestInteractor.Request(new LoadRandomizableRequestMessage(_orderedView.Order));
        }

        public bool IsInitialized { get; private set; }
        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            _actionHandler.OnAction = RequestRandomizable;
        }
    }
}