namespace Randomizer.UseCases
{
    public class RandomizableResponseMessage : ItemListResponseMessage
    {
        public string[] Labels { get; }

        public RandomizableResponseMessage(string title, string[] items, string[] labels)
        : base(ResponseType.DisplayRandomizable, title, items)
        {
            Labels = labels;
        }
    }
}