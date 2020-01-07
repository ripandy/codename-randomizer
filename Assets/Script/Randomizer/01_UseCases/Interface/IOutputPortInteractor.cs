using System;

namespace Randomizer.UseCases
{
    public interface IOutputPortInteractor
    {
        event Action OnResponse;
        void RaiseResponseEvent();
        
        ResponseType ResponseType { get; set; }
        string Title { get; set; }
        string[] Values { get; }
        int ValueCount { get; }
        void AddValue(string value);
        void ClearValue();
    }
}