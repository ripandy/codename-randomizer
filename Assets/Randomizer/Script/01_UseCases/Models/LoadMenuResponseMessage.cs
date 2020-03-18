namespace Randomizer.UseCases
{
    public class LoadMenuResponseMessage : ItemListResponseMessage
    {
        public int ActiveIndex { get; }
        
        public LoadMenuResponseMessage(string[] items, int activeIndex)
            : base(ResponseType.DisplayMenu, "", items)
        {
            ActiveIndex = activeIndex;
        }
    }
}