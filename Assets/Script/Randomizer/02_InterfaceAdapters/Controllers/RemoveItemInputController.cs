using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class RemoveItemInputController : IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IInputPortInteractor<int> _removeItemInteractor;
        private readonly IActionHandler<int> _actionHandler;
        
        private RemoveItemInputController(
            IInputPortInteractor<int> removeItemInteractor,
            IActionHandler<int> actionHandler)
        {
            _removeItemInteractor = removeItemInteractor;
            _actionHandler = actionHandler;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            _actionHandler.OnAction = _removeItemInteractor.InputHandler;
        }
    }
}