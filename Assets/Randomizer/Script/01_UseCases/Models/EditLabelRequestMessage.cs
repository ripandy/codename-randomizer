namespace Randomizer.UseCases
{
    public class EditLabelRequestMessage : IRequestMessage
    {
        public RequestType RequestType { get; }
        public int LabelIndex { get; }
        public string NewLabelName { get; }

        public EditLabelRequestMessage(int labelIndex, string newLabelName)
        {
            RequestType = RequestType.EditLabel;
            LabelIndex = labelIndex;
            NewLabelName = newLabelName;
        }
    }
}