using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResultPresenter : BasePresenter
    {
        private readonly ITextView _resultView;

        private ResultPresenter(
            IOutputPortInteractor responseInteractor,
            ITextView resultView)
            : base(responseInteractor)
        {
            _resultView = resultView;
        }

        protected override void OnResponse()
        {
            base.OnResponse();
            _resultView.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayResult;
        }

        protected override void OnDisplayResultResponse()
        {
            _resultView.Text = ResponseInteractor.Values[0];
        }
    }
}