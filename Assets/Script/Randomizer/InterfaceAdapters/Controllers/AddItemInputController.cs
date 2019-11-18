using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class AddItemInputController : IActionHandler<string>
    {
        private readonly IInputPortInteractor<string> _addItemRequestHandler;

        private AddItemInputController(IInputPortInteractor<string> addItemRequestHandler)
        {
            _addItemRequestHandler = addItemRequestHandler;
        }

        public void Handle(string itemName)
        {
            _addItemRequestHandler.Handle(itemName);
        }
    }
}