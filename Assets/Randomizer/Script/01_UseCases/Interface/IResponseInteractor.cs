using System;

namespace Randomizer.UseCases
{
    public interface IResponseInteractor
    {
        event Action<IResponseMessage> OnResponse;
        void Response(IResponseMessage responseMessage);
    }
}