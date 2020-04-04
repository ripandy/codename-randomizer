namespace Randomizer.UseCases
{
    public class PickLabelListResponseMessage : ItemListResponseMessage
    {
        public int[] PickedLabels { get; }
        
        public PickLabelListResponseMessage(string title, string[] items, int[] pickedLabels)
            : base(ResponseType.DisplayPickLabel, title, items)
        {
            PickedLabels = pickedLabels;
        }
    }
}