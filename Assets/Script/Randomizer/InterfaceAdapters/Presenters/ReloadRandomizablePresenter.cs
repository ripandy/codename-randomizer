using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ReloadRandomizablePresenter : IOutputPortInteractor<ReloadRandomizableResponseMessage>
    {
        private readonly RandomizablePresenter _randomizablePresenter;
        private readonly AddItemPresenter _addItemPresenter;

        private ReloadRandomizablePresenter(
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
            for (int i = 0; i < response.ItemNames.Length; i++)
            {
                var itemName = response.ItemNames[i];
                _randomizablePresenter.AddItem(itemName, i);
            }

            _addItemPresenter.ViewModelObject.Order = _randomizablePresenter.ItemCount;
        }
    }
}