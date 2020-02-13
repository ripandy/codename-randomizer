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
        private readonly IActionHandler _actionHandler;
        
        public TitlePresenter(
            IResponseInteractor responseInteractor,
            IActionHandler actionHandler,
            ITextView title) 
            : base(responseInteractor)
        {
            _actionHandler = actionHandler;
            _title = title;
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            UpdateTitle(responseMessage.Title);
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            var title = responseMessage.Title;
            var responseType = responseMessage.ResponseType;
            switch (responseType)
            {
                case ResponseType.DisplayResult:
                    title = $"{_defaultTitles[(int) responseType]}{(string.IsNullOrEmpty(title) ? "" : " of " + title)}";
                    break;
                case ResponseType.DisplayPickLabel:
                    title = $"{_defaultTitles[(int) responseType]}{(string.IsNullOrEmpty(title) ? "" : " for " + title)}";
                    break;
            }
            UpdateTitle(title);
        }

        protected override void PostResponse()
        {
            _actionHandler.Active = DisplayState == DisplayState.DisplayRandomizable;
        }

        private void UpdateTitle(string title)
        {
            _title.Text = string.IsNullOrEmpty(title) ? _defaultTitles[(int) DisplayState] : title;
        }
    }
}