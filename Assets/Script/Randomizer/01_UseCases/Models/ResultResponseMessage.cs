namespace Randomizer.UseCases
{
    public class ResultResponseMessage : IResponseMessage
    {
        public ResponseType ResponseType { get; }
        public string Title { get; }
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public ResultResponseMessage(string title, string[] items)
        {
            ResponseType = ResponseType.DisplayResult;
            Title = title;
            Items = items;
        }
    }
}