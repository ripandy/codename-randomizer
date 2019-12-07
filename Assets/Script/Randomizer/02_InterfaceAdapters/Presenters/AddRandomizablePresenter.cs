using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class AddRandomizablePresenter : BasePresenter
    {
        private readonly IView _button;
        
        private AddRandomizablePresenter(
            IView button,
            IList<IOutputPortInteractor> responses)
            : base(responses)
        {
            _button = button;
        }

        protected override void OnReload(ReloadResponseMessage responseMessage)
        {
            _button.Visible = !responseMessage.Success;
        }
    }
}