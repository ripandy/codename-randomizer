using Randomizer.InterfaceAdapters;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdaptor.Presenter
{
    public class ResultPresenter : IOutputPortInteractor<RandomizeResponseMessage>, IPresenter<ResultViewModel>
    {
        public ResultViewModel ViewModelObject { get; } = new ResultViewModel();
        public bool Visible { get; set; }

        private readonly RandomizablePresenter _randomizablePresenter;

        private ResultPresenter(RandomizablePresenter randomizablePresenter)
        {
            _randomizablePresenter = randomizablePresenter;
        }
        
        public void Handle(RandomizeResponseMessage response)
        {
            if (!response.Success) return; // TODO: show response message?

            ViewModelObject.ResultText = response.PickedItemName;
            Visible = response.Success;
            
            _randomizablePresenter.HideAllItems();
        }
    }
}