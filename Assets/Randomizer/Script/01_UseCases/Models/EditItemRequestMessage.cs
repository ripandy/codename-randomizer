namespace Randomizer.UseCases
{
    public class EditItemRequestMessage : IRequestMessage
    {
        public RequestType RequestType { get; }
        public int ItemIndex { get; }
        public string NewItemName { get; }

        public EditItemRequestMessage(int itemIndex, string newItemName)
        {
            RequestType = RequestType.EditItem;
            ItemIndex = itemIndex;
            NewItemName = newItemName;
        }
    }
}