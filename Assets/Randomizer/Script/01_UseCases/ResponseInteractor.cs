using System;

namespace Randomizer.UseCases
{
    public class ResponseInteractor : IResponseInteractor
    {
        public event Action<IResponseMessage> OnResponse;

        public void Response(IResponseMessage responseMessage)
        {
            OnResponse?.Invoke(responseMessage);
        }
    }
}