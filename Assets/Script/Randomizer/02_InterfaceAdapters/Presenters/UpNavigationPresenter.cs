using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class UpNavigationPresenter : BasePresenter
    {
        private readonly IView _upButtonView;
        
        public UpNavigationPresenter(
            IOutputPortInteractor responseInteractor,
            IView upButtonView)
            : base(responseInteractor)
        {
            _upButtonView = upButtonView;
        }

        protected override void OnResponse()
        {
            _upButtonView.Visible = ResponseInteractor.ResponseType != ResponseType.DisplayGroup;
        }
    }
}