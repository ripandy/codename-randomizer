using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizePresenter : BasePresenter
    {
        private readonly ITextView _button;

        private RandomizePresenter(
            IResponseInteractor responseInteractor,
            ITextView button)
            : base(responseInteractor)
        {
            _button = button;
        }

        protected override void OnResponse()
        {
            switch (ResponseInteractor.ResponseType)
            {
                case ResponseType.DisplayRandomizable:
                    _button.Text = "Randomize";
                    break;
                case ResponseType.DisplayLabel:
                    _button.Text = "Randomize All";
                    break;
            }
        }
    }
}