using System;

namespace Randomizer.UseCases
{
    public interface IOutputPortInteractor
    {
        event Action OnResponse;
        
        ResponseType ResponseType { get; }
        string Title { get; }
        string[] Values { get; }
        int ValueCount { get; }
    }
}