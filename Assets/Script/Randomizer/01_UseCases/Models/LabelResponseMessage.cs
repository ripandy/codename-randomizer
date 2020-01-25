namespace Randomizer.UseCases
{
    public class LabelResponseMessage : ResponseMessage<string>
    {
        public string Title => Value;
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public LabelResponseMessage(string title, string[] items)
        : base(ResponseType.DisplayLabel, title)
        {
            Items = items;
        }
    }
}