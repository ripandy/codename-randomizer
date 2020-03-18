namespace Randomizer.InterfaceAdapters.Presenters
{
    public class ItemPresenter
    {
        private readonly ITextView _textView;
        private readonly IOrderedView _orderedView;

        private ItemPresenter(
            ITextView textView,
            IOrderedView orderedView)
        {
            _textView = textView;
            _orderedView = orderedView;
        }
    }
}