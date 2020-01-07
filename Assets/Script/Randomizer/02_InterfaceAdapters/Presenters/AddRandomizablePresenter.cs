using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddRandomizablePresenter : BasePresenter
    {
        private readonly IView _button;

        private AddRandomizablePresenter(
            IOutputPortInteractor responseInteractor,
            IView button)
            : base(responseInteractor)
        {
            _button = button;
        }

        protected override void OnResponse()
        {
            _button.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayGroup;
        }
    }
}