using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResetResponseHandler : IOutputPortInteractor<ResetResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;
        private readonly ResultPresenter _resultPresenter;
        private readonly ClearPresenter _clearPresenter;
        private readonly ResetPresenter _resetPresenter;
        
        private ResetResponseHandler(
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
        
        public void Handle(ResetResponseMessage response)
        {
            // TODO : show response message?
            var success = response.Success;
            _randomizablePresenter.ShowAllItems(success);
            _addItemPresenter.Visible = success;
            _resultPresenter.Visible = !success;
            _clearPresenter.Visible = success;
            _resetPresenter.Visible = !success;
        }
    }
}