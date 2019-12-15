using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResetPresenter : BasePresenter
    {
        private readonly IView _resetButtonView;

        private ResetPresenter(
            IOutputPortInteractor responseInteractor,
            IView resetButtonView)
            : base(responseInteractor)
        {
            _resetButtonView = resetButtonView;
        }

        protected override void OnResponse()
        {
            _resetButtonView.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayResult;
        }
    }
}