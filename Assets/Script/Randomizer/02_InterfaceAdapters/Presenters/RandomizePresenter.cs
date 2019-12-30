using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizePresenter : BasePresenter
    {
        private readonly IView _button;

        private RandomizePresenter(
            IOutputPortInteractor responseInteractor,
            IView button)
            : base(responseInteractor)
        {
            _button = button;
        }

        protected override void OnResponse()
        {
            _button.Visible = ResponseInteractor.ResponseType == ResponseType.DisplayRandomizable ||
                              ResponseInteractor.ResponseType == ResponseType.DisplayResult;
        }
    }
}