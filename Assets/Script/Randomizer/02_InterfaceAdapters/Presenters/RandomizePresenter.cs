using Randomizer.UseCases;

namespace Randomizer.InterfaceAdapters.Presenters
{
    public class RandomizePresenter : BasePresenter
    {
        private const string Randomize = "Randomize";
        private const string RandomizeAll = Randomize + " All";
        
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
            _randomizeView.Text = Randomize;
        }

        protected override void OnResponse(ItemListResponseMessage responseMessage)
        {
            if (responseMessage.ResponseType != ResponseType.DisplayLabel) return;
            _randomizeView.Text = RandomizeAll;
        }
    }
}