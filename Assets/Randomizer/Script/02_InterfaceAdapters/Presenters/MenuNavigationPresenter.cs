using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class MenuNavigationPresenter : BasePresenter
    {
        private readonly IView _menuButtonView;
        
        public MenuNavigationPresenter(
            IResponseInteractor responseInteractor,
            IView menuButtonView)
            : base(responseInteractor)
        {
            _menuButtonView = menuButtonView;
        }

        protected override void PostResponse()
        {
            _menuButtonView.Visible =
                DisplayState == DisplayState.DisplayLabel
                || DisplayState == DisplayState.DisplayMenu;
        }
    }
}