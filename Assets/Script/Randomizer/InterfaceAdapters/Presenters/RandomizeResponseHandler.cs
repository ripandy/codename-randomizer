using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizeResponseHandler : IOutputPortInteractor<RandomizeResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;
        private readonly ResultPresenter _resultPresenter;
        private readonly ClearPresenter _clearPresenter;
        private readonly ResetPresenter _resetPresenter;

        private RandomizeResponseHandler(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter,
            ResultPresenter resultPresenter,
            ClearPresenter clearPresenter,
            ResetPresenter resetPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
            _resultPresenter = resultPresenter;
            _clearPresenter = clearPresenter;
            _resetPresenter = resetPresenter;
        }
        
        public void Handle(RandomizeResponseMessage response)
        {
            var success = response.Success;
            _resultPresenter.Visible = success;
            _addItemPresenter.Visible = !success;
            _randomizablePresenter.ShowAllItems(!success);
            _clearPresenter.Visible = !success;
            _resetPresenter.Visible = success;
            
            if (!response.Success) return; // TODO: show response message?

            _resultPresenter.ViewModelObject.ResultText = response.PickedItemName;
        }
    }
}