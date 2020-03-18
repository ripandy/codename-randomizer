using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class LabelListController : BaseController
    {
        private readonly IActionHandler<string> _editLabelActionHandler;
        private readonly IActionHandler _removeLabelActionHandler;
        private readonly IOrderedView _labelView;

        private LabelListController(
            IRequestInteractor requestInteractor,
            IList<IActionHandler> actionHandlers,
            IOrderedView labelView)
            : base(requestInteractor, actionHandlers)
        {
            _editLabelActionHandler = ActionHandlers[0] as IActionHandler<string>;
            _removeLabelActionHandler = ActionHandlers[1];
            _labelView = labelView;
        }
        
        protected override void BindAction()
        {       
            _editLabelActionHandler.OnAction = () => RequestEditLabel(_editLabelActionHandler.Value);
            _removeLabelActionHandler.OnAction = RequestRemoveLabel;
        }

        private void RequestEditLabel(string value)
        {
            var index = _labelView.Order;
            if (string.IsNullOrEmpty(value))
                RequestRemoveLabel();
            else
                RequestInteractor.Request(new EditLabelRequestMessage(index, value));
        }

        private void RequestRemoveLabel()
        {
            RequestInteractor.Request(new RequestMessage<int>(RequestType.RemoveLabel, _labelView.Order));
        }
    }
}