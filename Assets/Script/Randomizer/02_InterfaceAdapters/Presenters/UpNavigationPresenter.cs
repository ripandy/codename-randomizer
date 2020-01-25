using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class UpNavigationPresenter : BasePresenter
    {
        private readonly IView _upButtonView;
        
        public UpNavigationPresenter(
            IResponseInteractor responseInteractor,
            IView upButtonView)
            : base(responseInteractor)
        {
            _upButtonView = upButtonView;
        }

        protected override void FinalizeResponse()
        {
            _upButtonView.Visible = DisplayState != DisplayState.DisplayLabel;
        }
    }
}