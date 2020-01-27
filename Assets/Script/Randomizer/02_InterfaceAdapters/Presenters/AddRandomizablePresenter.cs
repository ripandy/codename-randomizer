using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddRandomizablePresenter : BasePresenter
    {
        private readonly IOrderedView _addRandomizableView;

        private AddRandomizablePresenter(
            IResponseInteractor responseInteractor,
            IOrderedView addRandomizableView)
            : base(responseInteractor)
        {
            _addRandomizableView = addRandomizableView;
        }

        protected override void OnResponse(LabelResponseMessage responseMessage)
        {
            _addRandomizableView.Order = responseMessage.ItemCount;
        }

        protected override void FinalizeResponse()
        {
            _addRandomizableView.Visible = DisplayState == DisplayState.DisplayLabel;
        }
    }
}