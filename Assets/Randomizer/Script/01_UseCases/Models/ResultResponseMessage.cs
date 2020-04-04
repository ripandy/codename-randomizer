namespace Randomizer.UseCases
{
    public class ResultResponseMessage : ItemListResponseMessage
    {
        public ResultResponseMessage(string title, string[] items)
        : base(ResponseType.DisplayResult, title, items)
        {
        }
    }
}