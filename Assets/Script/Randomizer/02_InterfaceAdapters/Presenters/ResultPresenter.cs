using System.Collections.Generic;
using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ResultPresenter : BasePresenter
    {
        private readonly ITextView _resultView;
        private ResultPresenter(
            ITextView resultView,
            IList<IOutputPortInteractor> responses)
        : base(responses)
        {
            _resultView = resultView;
        }

        protected override void OnRandomize(RandomizeResponseMessage responseMessage)
        {
            _resultView.Visible = responseMessage.Success;

            if (!responseMessage.Success) return;
            
            _resultView.Text = responseMessage.Message;
        }
    }
}