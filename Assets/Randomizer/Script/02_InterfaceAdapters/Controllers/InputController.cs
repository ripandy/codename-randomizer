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
                if (type == RequestType.DeselectLabel)
                    BindDeselectLabelAction(ActionHandlers[i]);
                else if (ActionHandlers[i] is IActionHandler<string> stringAction)
                    BindAction(type, stringAction);
                else
                    BindAction(type, ActionHandlers[i]);
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
        
        private void BindDeselectLabelAction(IActionHandler actionHandler)
        {
            actionHandler.OnAction = () => RequestInteractor.Request(new LoadLabelRequestMessage(-1));
        }
    }
}