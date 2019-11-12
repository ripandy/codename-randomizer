using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResultPresenter : IPresenter<ResultViewModel>
    {
        public ResultViewModel ViewModelObject { get; } = new ResultViewModel();
        public bool Visible { get; set; }
    }
}