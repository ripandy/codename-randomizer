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
            var success = responseMessage.Success;
            _resultView.Visible = success;
            
            if (!success) return;
            
            _resultView.Text = responseMessage.Message;
        }
        
        protected override void OnReload(ReloadResponseMessage responseMessage)
        {
            _resultView.Visible = false;
        }
    }
}