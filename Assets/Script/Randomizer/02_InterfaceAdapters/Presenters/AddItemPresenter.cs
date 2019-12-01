using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddItemPresenter : BasePresenter
    {
        private readonly IOrderedView _addItemView;

        private AddItemPresenter(
            IOrderedView addItemView,
            IList<IOutputPortInteractor> responses)
        : base(responses)
        {
            _addItemView = addItemView;
        }

        protected override void OnAddItem(AddItemResponseMessage response)
        {
            if (!response.Success) return; // TODO : show response message?
            _addItemView.Order = response.NewItemOrder + 1;
        }
        
        protected override void OnRandomize(RandomizeResponseMessage responseMessage)
        {
            _addItemView.Visible = !responseMessage.Success;
        }

        protected override void OnReload(ReloadResponseMessage responseMessage)
        {
            _addItemView.Order = responseMessage.ItemNames.Length;
        }
    }
}