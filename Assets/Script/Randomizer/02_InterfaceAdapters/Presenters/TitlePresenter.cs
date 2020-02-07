using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class TitlePresenter : BasePresenter
    {
        private readonly string[] _defaultTitles = {
            "Randomizer",
            "",
            "Select Label",
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

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            var title = responseMessage.Title;
            switch (responseMessage.ResponseType)
            {
                case ResponseType.DisplayResult:
                    title = $"{_defaultTitles[(int) DisplayState]}{(string.IsNullOrEmpty(title) ? "" : " of " + title)}";
                    break;
                case ResponseType.DisplayPickLabel:
                    title = $"{_defaultTitles[(int) DisplayState]}{(string.IsNullOrEmpty(title) ? "" : " for " + title)}";
                    break;
            }
            UpdateTitle(title);
        }

        private void UpdateTitle(string title)
        {
            _title.Text = string.IsNullOrEmpty(title) ? _defaultTitles[(int) DisplayState] : title;
        }
    }
}