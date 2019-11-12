using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResultPresenter : IOutputPortInteractor<RandomizeResponseMessage>, IPresenter<ResultViewModel>
    {
        public ResultViewModel ViewModelObject { get; } = new ResultViewModel();
        public bool Visible { get; set; }

        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;

        private ResultPresenter(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
        }
        
        public void Handle(RandomizeResponseMessage response)
        {
            if (!response.Success) return; // TODO: show response message?

            ViewModelObject.ResultText = response.PickedItemName;
            Visible = response.Success;
            
            _randomizablePresenter.HideAllItems();
            _addItemPresenter.Visible = false;
        }
    }
}