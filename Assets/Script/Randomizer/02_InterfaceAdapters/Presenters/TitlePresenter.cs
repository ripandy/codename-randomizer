using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class TitlePresenter : BasePresenter
    {
        private readonly string[] _defaultTitles = {
            "Randomizer",
            "",
            "Result",
            "Results"
        };
        
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
            var title = $" of {responseMessage.Title}";
            _title.Text = $"{_defaultTitles[(int) DisplayState]}{(string.IsNullOrEmpty(responseMessage.Title) ? "" : title)}";
        }

        private void UpdateTitle(string title)
        {
            _title.Text = string.IsNullOrEmpty(title) ? _defaultTitles[(int) DisplayState] : title;
        }
    }
}