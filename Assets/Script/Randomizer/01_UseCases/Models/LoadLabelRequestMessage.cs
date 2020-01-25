namespace Randomizer.UseCases
{
    public class LoadLabelRequestMessage : RequestMessage<int>
    {
        public static readonly LoadLabelRequestMessage RequestActive = new LoadLabelRequestMessage();
        public int LabelId => Value;
        public bool LoadActive { get; }

        private LoadLabelRequestMessage()
        : base(RequestType.LoadRandomizable, -1)
        {
            LoadActive = true;
        }
        
        public LoadLabelRequestMessage(int labelId)
        : base(RequestType.LoadRandomizable, labelId)
        {
            LoadActive = false;
        }
        
    }
}