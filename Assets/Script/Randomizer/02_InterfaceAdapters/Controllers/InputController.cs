using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class InputController : IInitializable
    {
        private readonly IRequestInteractor _requestInteractor;
        private readonly IList<IActionHandler> _actionHandlers;
        private readonly IList<RequestType> _requestTypes;

        private InputController (
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IList<RequestType> requestTypes)
        {
            _requestInteractor = requestInteractor;
            _actionHandlers = actionHandlers;
            _requestTypes = requestTypes;
        }
        
        private void BindAction(RequestType type, IActionHandler actionHandler)
        {
            actionHandler.OnAction = () => _requestInteractor.Request(new RequestMessage(type));
        }

        private void BindAction<T>(RequestType type, IActionHandler<T> actionHandler)
        {
            actionHandler.OnAction = () => _requestInteractor.Request(new RequestMessage<T>(type, actionHandler.Value));
        }

        public bool IsInitialized { get; private set; }
        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
                
            for (var i = 0; i < _requestTypes.Count; i++)
            {
                var type = _requestTypes[i];
                switch (type)
                {
                    case RequestType.AddItem:
                    case RequestType.EditTitle:
                        BindAction(type, _actionHandlers[i] as IActionHandler<string>);
                        break;
                    case RequestType.AddRandomizable:
                    case RequestType.Randomize:
                        BindAction(type, _actionHandlers[i]);
                        break;
                }
            }
        }
    }
}