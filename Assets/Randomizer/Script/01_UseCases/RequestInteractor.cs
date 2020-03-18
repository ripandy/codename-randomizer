using System;

namespace Randomizer.UseCases
{
    public class RequestInteractor : IRequestInteractor
    {
        public event Action<IRequestMessage> OnRequest;
        
        public void Request(IRequestMessage requestMessage)
        {
            OnRequest?.Invoke(requestMessage);
        }
    }
}