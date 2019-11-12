using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class AddItemInputController : IActionHandler<string>
    {
        private readonly IInputPortInteractor<AddItemRequestMessage> _addItemRequestHandler;

        private AddItemInputController(IInputPortInteractor<AddItemRequestMessage> addItemRequestHandler)
        {
            _addItemRequestHandler = addItemRequestHandler;
        }

        public void Handle(string itemName)
        {
            _addItemRequestHandler.Handle(new AddItemRequestMessage {ItemName = itemName});
        }
    }
}