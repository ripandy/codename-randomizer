using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ReloadRandomizableResponseHandler : IOutputPortInteractor<ReloadRandomizableResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;

        private ReloadRandomizableResponseHandler(
            RandomizablePresenter randomizablePresenter,
            AddItemPresenter addItemPresenter)
        {
            _randomizablePresenter = randomizablePresenter;
            _addItemPresenter = addItemPresenter;
        }
        
        public void Handle(ReloadRandomizableResponseMessage response)
        {
            if (!response.Success) return; // TODO : show response message?

            _randomizablePresenter.Clear();
            if (response.ItemNames != null)
            {
                for (int i = 0; i < response.ItemNames.Length; i++)
                {
                    var itemName = response.ItemNames[i];
                    _randomizablePresenter.AddItem(itemName, i);
                }
            }

            _addItemPresenter.ViewModelObject.Order = _randomizablePresenter.ItemCount;
        }
    }
}