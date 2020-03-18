namespace Randomizer.UseCases
{
    public class EditLabelRequestMessage : IRequestMessage
    {
        public RequestType RequestType { get; }
        public int LabelId { get; }
        public string NewLabelName { get; }

        public EditLabelRequestMessage(int labelId, string newLabelName)
        {
            RequestType = RequestType.EditLabel;
            LabelId = labelId;
            NewLabelName = newLabelName;
        }
    }
}