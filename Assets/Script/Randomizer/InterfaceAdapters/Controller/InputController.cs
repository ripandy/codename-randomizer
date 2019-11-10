using System;
using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controller
{
    public class InputController
    {
        private readonly IInputPortInteractor<AddItemRequestMessage> _addItemRequestHandler;
        private readonly IList<IActionHandler<string>> _inputFields;
        private readonly IList<IButtonHandler> _buttons;

        private InputController(
            IInputPortInteractor<AddItemRequestMessage> addItemRequestHandler,
            IList<IActionHandler<string>> inputFields,
            IList<IButtonHandler> buttons)
        {
            _addItemRequestHandler = addItemRequestHandler;
            _inputFields = inputFields;
            _buttons = buttons;
            
            Initialize();
        }

        private void Initialize()
        {
            foreach (InputFieldTypeEnum requestType in Enum.GetValues(typeof(InputFieldTypeEnum)))
            {
                if (requestType == InputFieldTypeEnum.AddItem)
                    _inputFields[(int)requestType].Subscribe(RequestAddItem);
            }
        }

        private void RequestAddItem(string itemName)
        {
            _addItemRequestHandler.Handle(new AddItemRequestMessage {ItemName = itemName});
        }
    }
}