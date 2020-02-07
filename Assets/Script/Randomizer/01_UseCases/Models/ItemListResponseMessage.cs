namespace Randomizer.UseCases
{
    public class ItemListResponseMessage : ResponseMessage
    {
        public string Title { get; }
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public ItemListResponseMessage(ResponseType responseType, string title, string[] items)
            : base(responseType)
        {
            Title = title;
            Items = items;
        }
    }
}