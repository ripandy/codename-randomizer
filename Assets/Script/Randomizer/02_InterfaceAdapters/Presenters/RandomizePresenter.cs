using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizePresenter : BasePresenter
    {
        private readonly IView _button;
        
        private RandomizePresenter(
            IView button,
            IList<IOutputPortInteractor> responses)
            : base(responses)
        {
            _button = button;
        }

        protected override void OnReload(ReloadResponseMessage responseMessage)
        {
            _button.Visible = responseMessage.Success;
        }
    }
}