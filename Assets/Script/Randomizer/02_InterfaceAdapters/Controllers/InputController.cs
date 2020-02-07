using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class InputController : BaseController
    {
        private readonly IList<RequestType> _requestTypes;

        private InputController(
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IList<RequestType> requestTypes)
            : base(requestInteractor, actionHandlers)
        {
            _requestTypes = requestTypes;
        }
        
        protected override void BindAction()
        {
            for (var i = 0; i < _requestTypes.Count; i++)
            {
                var type = _requestTypes[i];
                switch (type)
                {
                    case RequestType.AddItem:
                    case RequestType.EditTitle:
                        BindAction(type, ActionHandlers[i] as IActionHandler<string>);
                        break;
                    case RequestType.AddRandomizable:
                    case RequestType.Randomize:
                        BindAction(type, ActionHandlers[i]);
                        break;
                }
            }
        }

        private void BindAction(RequestType type, IActionHandler actionHandler)
        {
            actionHandler.OnAction = () => RequestInteractor.Request(new RequestMessage(type));
        }

        private void BindAction<T>(RequestType type, IActionHandler<T> actionHandler)
        {
            actionHandler.OnAction = () => RequestInteractor.Request(new RequestMessage<T>(type, actionHandler.Value));
        }
    }
}