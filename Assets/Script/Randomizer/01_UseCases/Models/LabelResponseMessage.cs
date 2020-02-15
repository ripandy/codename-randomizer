namespace Randomizer.UseCases
{
    public class LabelResponseMessage : ItemListResponseMessage
    {
        public LabelResponseMessage(string title, string[] items)
        : base(ResponseType.DisplayLabel, title, items)
        {
        }
    }
}