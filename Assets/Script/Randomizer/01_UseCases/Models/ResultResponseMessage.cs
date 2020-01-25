namespace Randomizer.UseCases
{
    public class ResultResponseMessage : ResponseMessage<string>
    {
        public string Title => Value;
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public ResultResponseMessage(string title, string[] items)
        : base(ResponseType.DisplayResult, title)
        {
            Items = items;
        }
    }
}