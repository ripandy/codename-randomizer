namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResetClearPresenter : IPresenter<ResetClearViewModel>
    {
        public ResetClearViewModel ViewModelObject { get; } = new ResetClearViewModel();
        public bool Visible { get; set; } = true;
    }
}