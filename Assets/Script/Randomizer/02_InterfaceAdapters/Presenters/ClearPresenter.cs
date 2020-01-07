using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ClearPresenter : BasePresenter
    {
        private readonly IView _clearButtonView;

        private ClearPresenter(
            IOutputPortInteractor responseInteractor,
            IView clearButtonView)
            : base(responseInteractor)
        {
            _clearButtonView = clearButtonView;
        }

        protected override void OnResponse()
        {
            _clearButtonView.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayRandomizable &&
                                       ResponseInteractor.ValueCount > 0;
        }
    }
}