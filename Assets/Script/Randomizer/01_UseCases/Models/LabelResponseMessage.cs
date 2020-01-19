namespace Randomizer.UseCases
{
    public class LabelResponseMessage : IResponseMessage
    {
        public ResponseType ResponseType { get; }
        public string Title { get; }
        public string[] Items { get; }

        public LabelResponseMessage(string title, string[] items)
        {
            ResponseType = ResponseType.DisplayLabel;
            Title = title;
            Items = items;
        }
    }
}