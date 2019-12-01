using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResetPresenter  : BasePresenter
    {
        private readonly IView _resetButtonView;

        private ResetPresenter(
            IView resetButtonView,
            IList<IOutputPortInteractor> responses)
            : base(responses)
        {
            _resetButtonView = resetButtonView;
        }

        protected override void OnRandomize(RandomizeResponseMessage responseMessage)
        {
            _resetButtonView.Visible = responseMessage.Success;
        }
    }
}