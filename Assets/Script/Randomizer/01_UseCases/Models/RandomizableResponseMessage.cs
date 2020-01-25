namespace Randomizer.UseCases
{
    public class RandomizableResponseMessage : ResponseMessage<string>
    {
        public string Title => Value;
        public string[] Items { get; }
        public int ItemCount => Items.Length;

        public RandomizableResponseMessage(string title, string[] items)
        : base(ResponseType.DisplayRandomizable, title)
        {
            Items = items;
        }
    }
}