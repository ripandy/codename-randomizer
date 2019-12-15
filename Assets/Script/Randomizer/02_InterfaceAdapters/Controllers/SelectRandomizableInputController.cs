using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class SelectRandomizableInputController : IInitializable
    {
        private readonly IInputPortInteractor<int> _sessionLoaderInteractor;
        private readonly IActionHandler<int> _actionHandler;
        
        private SelectRandomizableInputController(
            IInputPortInteractor<int> sessionLoaderInteractor,
            IActionHandler<int> actionHandler)
        {
            _sessionLoaderInteractor = sessionLoaderInteractor;
            _actionHandler = actionHandler;
        }

        public void Initialize()
        {
            _actionHandler.OnAction = _sessionLoaderInteractor.InputHandler;
        }
    }
}