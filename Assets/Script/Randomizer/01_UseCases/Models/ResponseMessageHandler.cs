using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.UseCases
{
    public enum ResponseType
    {
        None = -1,
        DisplayResult,
        DisplayRandomizable,
        DisplayGroup,
    }
    
    public class ResponseMessageHandler : IOutputPortInteractor
    {
        private readonly IList<string> _values = new List<string>();
     
        public event Action OnResponse;   
        public void RaiseResponseEvent() => OnResponse?.Invoke();
        
        public ResponseType ResponseType { get; set; } = ResponseType.None;
        public string Title { get; set; }
        public string[] Values => _values.ToArray();
        public int ValueCount => _values.Count;
        public void AddValue(string value) => _values.Add(value);
        public void RemoveValue(int index) => _values.RemoveAt(index);
        public void ClearValue() => _values.Clear();
    }
}