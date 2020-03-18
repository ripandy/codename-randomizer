namespace Randomizer.UseCases
{
    public class EditItemRequestMessage : IRequestMessage
    {
        public RequestType RequestType { get; }
        public int ItemId { get; }
        public string NewItemName { get; }

        public EditItemRequestMessage(int itemId, string newItemName)
        {
            RequestType = RequestType.EditItem;
            ItemId = itemId;
            NewItemName = newItemName;
        }
    }
}