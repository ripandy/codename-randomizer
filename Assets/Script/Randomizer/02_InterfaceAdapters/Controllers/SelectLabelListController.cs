using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Controllers
{
    public class SelectLabelListController : BaseController
    {
        private readonly IOrderedView _orderedView;
        
        public SelectLabelListController(
            IRequestInteractor requestInteractor,
            IActionHandler actionHandler,
            IOrderedView orderedView)
            : base(requestInteractor, actionHandler)
        {
            _orderedView = orderedView;
        }

        protected override void BindAction()
        {
            ActionHandler.OnAction = RequestSelectLabel;
        }

        private void RequestSelectLabel()
        {
            RequestInteractor.Request(new LoadLabelRequestMessage(_orderedView.Order));
        }
    }
}