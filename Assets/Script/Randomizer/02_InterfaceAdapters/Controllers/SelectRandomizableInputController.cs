using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class SelectRandomizableInputController
    {
        private SelectRandomizableInputController(
            IInputPortInteractor<int> sessionLoaderInteractor,
            IActionHandler<int> actionHandler)
        {
            actionHandler.OnAction = sessionLoaderInteractor.InputHandler;
        }
    }
}