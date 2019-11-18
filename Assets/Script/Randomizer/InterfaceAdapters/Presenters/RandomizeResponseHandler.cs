using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizeResponseHandler : IOutputPortInteractor<RandomizeResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;
        private readonly ResultPresenter _resultPresenter;
        private readonly ResetClearPresenter _resetClearPresenter;

        private RandomizeResponseHandler(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter,
            ResultPresenter resultPresenter,
            ResetClearPresenter resetClearPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
            _resultPresenter = resultPresenter;
            _resetClearPresenter = resetClearPresenter;
        }
        
        public void Handle(RandomizeResponseMessage response)
        {
            var success = response.Success;
            _resultPresenter.Visible = success;
            _addItemPresenter.Visible = !success;
            _randomizablePresenter.ShowAllItems(!success);
            _resetClearPresenter.Visible = !success;
            _resetClearPresenter.ViewModelObject.State = response.Success
                ? ResetClearViewModel.ButtonState.Reset
                : ResetClearViewModel.ButtonState.Clear;
            
            if (!response.Success) return; // TODO: show response message?

            _resultPresenter.ViewModelObject.ResultText = response.Message;
        }
    }
}