namespace Randomizer.UseCases
{
    public class LoadLabelRequestMessage : RequestMessage<int>
    {
        public static readonly LoadLabelRequestMessage RequestActive = new LoadLabelRequestMessage();
        public int LabelIndex => Value;
        public bool LoadActive { get; }

        private LoadLabelRequestMessage()
        : base(RequestType.LoadLabel, -1)
        {
            LoadActive = true;
        }
        
        public LoadLabelRequestMessage(int labelId)
        : base(RequestType.LoadLabel, labelId)
        {
            LoadActive = false;
        }
        
    }
}