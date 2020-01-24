using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class InputController
    {
        private readonly IRequestInteractor _requestInteractor;

        protected InputController (
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IList<RequestType> types)
        {
            _requestInteractor = requestInteractor;
            for (var i = 0; i < types.Count; i++)
            {
                var type = types[i];
                switch (type)
                {
                    case RequestType.AddItem:
                    case RequestType.EditTitle:
                        BindAction(type, actionHandlers[i] as IActionHandler<string>);
                        break;
                    case RequestType.AddRandomizable:
                    case RequestType.Randomize:
                    case RequestType.UpNavigation:
                        BindAction(type, actionHandlers[i]);
                        break;
                }
            }
        }
        
        private void BindAction(RequestType type, IActionHandler actionHandler)
        {
            actionHandler.OnAction = () => _requestInteractor.Request(new RequestMessage(type));
        }

        private void BindAction<T>(RequestType type, IActionHandler<T> actionHandler)
        {
            actionHandler.OnAction = () => _requestInteractor.Request(new RequestMessage<T>(type, actionHandler.Value));
        }
    }
}