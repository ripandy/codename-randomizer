using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddRandomizablePresenter : BasePresenter
    {
        private readonly IOrderedView _button;

        private AddRandomizablePresenter(
            IOutputPortInteractor responseInteractor,
            IOrderedView button)
            : base(responseInteractor)
        {
            _button = button;
        }

        protected override void OnResponse()
        {
            base.OnResponse();
            _button.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayLabel;
        }

        protected override void OnDisplayGroupResponse()
        {
            _button.Order = ResponseInteractor.ValueCount;
        }
    }
}