using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemPresenter : BasePresenter
    {
        private readonly IOrderedView _addItemView;

        private AddItemPresenter(
            IOutputPortInteractor responseInteractor,
            IOrderedView addItemView)
            : base(responseInteractor)
        {
            _addItemView = addItemView;
        }

        protected override void OnResponse()
        {
            base.OnResponse();
            _addItemView.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayRandomizable;
        }

        protected override void OnDisplayRandomizableResponse()
        {
            _addItemView.Order = ResponseInteractor.ValueCount; 
        }
    }
}