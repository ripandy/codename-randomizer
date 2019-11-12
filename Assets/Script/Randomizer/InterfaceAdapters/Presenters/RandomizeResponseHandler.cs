using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizeResponseHandler : IOutputPortInteractor<RandomizeResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;
        private readonly ResultPresenter _resultPresenter;

        private RandomizeResponseHandler(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter,
            ResultPresenter resultPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
            _resultPresenter = resultPresenter;
        }
        
        public void Handle(RandomizeResponseMessage response)
        {
            _resultPresenter.Visible = response.Success;
            _addItemPresenter.Visible = !response.Success;
            
            if (!response.Success) return; // TODO: show response message?

            _resultPresenter.ViewModelObject.ResultText = response.PickedItemName;
            _randomizablePresenter.HideAllItems();
        }
    }
}