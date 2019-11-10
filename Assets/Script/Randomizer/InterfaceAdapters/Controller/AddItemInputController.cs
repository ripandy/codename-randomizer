using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controller
{
    public class AddItemInputController
    {
        private readonly IInputPortInteractor<AddItemRequestMessage> _addItemRequestHandler;
        private readonly IActionHandler<string> _inputField;

        private AddItemInputController(
            IInputPortInteractor<AddItemRequestMessage> addItemRequestHandler,
            IActionHandler<string> inputField)
        {
            _addItemRequestHandler = addItemRequestHandler;
            _inputField = inputField;
            
            Initialize();
        }

        private void Initialize()
        {
            _inputField.Subscribe(RequestAddItem);
        }

        private void RequestAddItem(string itemName)
        {
            _addItemRequestHandler.Handle(new AddItemRequestMessage {ItemName = itemName});
        }
    }
}