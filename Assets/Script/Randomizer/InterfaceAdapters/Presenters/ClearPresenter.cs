using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ClearPresenter : BasePresenter
    {
        private readonly IView _clearButtonView;

        private ClearPresenter(
            IView clearButtonView,
            IList<IOutputPortInteractor> responses)
        : base(responses)
        {
            _clearButtonView = clearButtonView;
        }

        protected override void OnAddItem(AddItemResponseMessage responseMessage)
        {
            if (!responseMessage.Success) return;
            _clearButtonView.Visible = true;
        }

        protected override void OnRandomize(RandomizeResponseMessage responseMessage)
        {
            _clearButtonView.Visible = !responseMessage.Success;
        }

        protected override void OnReload(ReloadResponseMessage response)
        {
            _clearButtonView.Visible = response.Success && response.ItemNames.Length > 0;
        }
    }
}