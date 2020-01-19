namespace Randomizer.UseCases
{
    public class RandomizableResponseMessage : IResponseMessage
    {
        public ResponseType ResponseType { get; }
        public string Title { get; }
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public RandomizableResponseMessage(string title, string[] items)
        {
            ResponseType = ResponseType.DisplayRandomizable;
            Title = title;
            Items = items;
        }
    }
}