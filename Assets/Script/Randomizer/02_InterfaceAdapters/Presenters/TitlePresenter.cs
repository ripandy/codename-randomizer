using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class TitlePresenter : BasePresenter
    {
        private const string DefaultTitle = "Randomizer";
        
        private readonly ITextView _title;
        
        public TitlePresenter(
            IOutputPortInteractor responseInteractor,
            ITextView title) 
            : base(responseInteractor)
        {
            _title = title;
        }

        protected override void OnResponse()
        {
            _title.Text =
                string.IsNullOrEmpty(ResponseInteractor.Title) &&
                ResponseInteractor.ResponseType != ResponseType.DisplayRandomizable
                    ? DefaultTitle
                    : ResponseInteractor.Title;
        }
    }
}