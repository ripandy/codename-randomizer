using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class SelectRandomizableInputController : IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IRequestInteractor _sessionLoaderInteractor;
        private readonly IActionHandler<int> _actionHandler;
        
        private SelectRandomizableInputController(
            IRequestInteractor sessionLoaderInteractor,
            IActionHandler<int> actionHandler)
        {
            _sessionLoaderInteractor = sessionLoaderInteractor;
            _actionHandler = actionHandler;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            _actionHandler.OnAction = _sessionLoaderInteractor.HandleRequest;
        }
    }
}