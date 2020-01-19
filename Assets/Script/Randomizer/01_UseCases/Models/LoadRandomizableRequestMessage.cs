namespace Randomizer.UseCases
{
    public class LoadRandomizableRequestMessage : IRequestMessage
    {
        public static readonly LoadRandomizableRequestMessage RequestActive = new LoadRandomizableRequestMessage();
        public RequestType RequestType { get; }
        public int RandomizableId { get; }
        public bool LoadActive { get; }

        private LoadRandomizableRequestMessage()
        {
            RequestType = RequestType.LoadRandomizable;
            RandomizableId = -1;
            LoadActive = true;
        }
        
        public LoadRandomizableRequestMessage(int randomizableId)
        {
            RequestType = RequestType.LoadRandomizable;
            RandomizableId = randomizableId;
            LoadActive = false;
        }
    }
}