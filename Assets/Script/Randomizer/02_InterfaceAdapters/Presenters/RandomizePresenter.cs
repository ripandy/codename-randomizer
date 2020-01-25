using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizePresenter : BasePresenter
    {
        private readonly ITextView _randomizeView;

        private RandomizePresenter(
            IResponseInteractor responseInteractor,
            ITextView randomizeView)
            : base(responseInteractor)
        {
            _randomizeView = randomizeView;
        }

        protected override void OnResponse(RandomizableResponseMessage responseMessage)
        {
            _randomizeView.Text = "Randomize";
        }

        protected override void OnResponse(LabelResponseMessage responseMessage)
        {
            _randomizeView.Text = "Randomize All";
        }
    }
}