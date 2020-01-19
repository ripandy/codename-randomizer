namespace Randomizer.UseCases
{
    public class LoadLabelRequestMessage : IRequestMessage
    {
        public static readonly LoadLabelRequestMessage RequestActive = new LoadLabelRequestMessage();
        public RequestType RequestType { get; }
        public int LabelId { get; }
        public bool LoadActive { get; }

        private LoadLabelRequestMessage()
        {
            RequestType = RequestType.LoadRandomizable;
            LabelId = -1;
            LoadActive = true;
        }
        
        public LoadLabelRequestMessage(int labelId)
        {
            RequestType = RequestType.LoadRandomizable;
            LabelId = labelId;
            LoadActive = false;
        }
        
    }
}