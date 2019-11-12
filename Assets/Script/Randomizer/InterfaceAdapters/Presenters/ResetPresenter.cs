namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResetPresenter : IPresenter<ResetViewModel>
    {
        public bool Visible { get; set; } = true;
        public ResetViewModel ViewModelObject { get; } = new ResetViewModel { Caption = "Reset" };
    }
}