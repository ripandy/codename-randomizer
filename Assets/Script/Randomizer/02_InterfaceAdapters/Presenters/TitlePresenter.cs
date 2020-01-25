using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class TitlePresenter : BasePresenter
    {
        private const string DefaultTitle = "Randomizer";
        
        private readonly ITextView _title;
        
        public TitlePresenter(
            IResponseInteractor responseInteractor,
            ITextView title) 
            : base(responseInteractor)
        {
            _title = title;
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            UpdateTitle(responseMessage.Title);
        }

        protected override void OnResponse(LabelResponseMessage responseMessage)
        {
            UpdateTitle(responseMessage.Title);
        }

        protected override void OnResponse(ResultResponseMessage responseMessage)
        {
            UpdateTitle(responseMessage.Title);
        }

        private void UpdateTitle(string title)
        {
            _title.Text = string.IsNullOrEmpty(title) ? DefaultTitle : title;
        }
    }
}