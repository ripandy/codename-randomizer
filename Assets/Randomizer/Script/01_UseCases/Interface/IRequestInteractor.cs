using System;

namespace Randomizer.UseCases
{
    public interface IRequestInteractor
    {
        event Action<IRequestMessage> OnRequest;
        void Request(IRequestMessage requestMessage);
    }
}