using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddLabelPresenter : BasePresenter
    {
        private readonly IOrderedView _addLabelView;

        private AddLabelPresenter(
            IResponseInteractor responseInteractor,
            IOrderedView addLabelView)
            : base(responseInteractor)
        {
            _addLabelView = addLabelView;
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            if (responseMessage.ResponseType != ResponseType.DisplayManageLabel) return;
            _addLabelView.Order = responseMessage.ItemCount;
        }

        protected override void PostResponse()
        {
            _addLabelView.Visible = DisplayState == DisplayState.DisplayManageLabel;
        }
    }
}