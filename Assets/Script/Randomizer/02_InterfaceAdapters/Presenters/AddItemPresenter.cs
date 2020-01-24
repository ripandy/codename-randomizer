using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemPresenter : BasePresenter
    {
        private readonly IOrderedView _addItemView;

        private AddItemPresenter(
            IResponseInteractor responseInteractor,
            IOrderedView addItemView)
            : base(responseInteractor)
        {
            _addItemView = addItemView;
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            _addItemView.Visible = DisplayState == DisplayState.DisplayRandomizable;
            _addItemView.Order = responseMessage.ItemCount;
        }
    }
}