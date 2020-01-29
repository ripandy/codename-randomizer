namespace Randomizer.UseCases
{
    public class LoadRandomizableRequestMessage : RequestMessage<int>
    {
        public static readonly LoadRandomizableRequestMessage RequestActive = new LoadRandomizableRequestMessage();
        public int RandomizableId => Value;
        public bool LoadActive { get; }

        private LoadRandomizableRequestMessage()
        : base(RequestType.LoadRandomizable, -1)
        {
            LoadActive = true;
        }
        
        public LoadRandomizableRequestMessage(int randomizableId)
        : base(RequestType.LoadRandomizable, randomizableId)
        {
            LoadActive = false;
        }
    }
}