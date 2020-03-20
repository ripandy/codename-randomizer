using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RemoveRandomizablePresenter : BasePresenter
    {
        private readonly IView _removeButtonView;
        
        public RemoveRandomizablePresenter(
            IResponseInteractor responseInteractor,
            IView removeButtonView)
            : base(responseInteractor)
        {
            _removeButtonView = removeButtonView;
        }

        protected override void PostResponse()
        {
            _removeButtonView.Visible = DisplayState == DisplayState.DisplayRandomizable;
        }
    }
}