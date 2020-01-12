using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;
using Sirenix.Utilities;

namespace Randomizer.UseCases
{
    public class ResponseInteractor : IResponseInteractor, IOutputPortInteractor
    {
        private readonly IList<string> _values = new List<string>();
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;

        private ResponseInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
        }
     
        public event Action OnResponse;

        public ResponseType ResponseType { get; private set; }
        public string Title { get; private set; }
        public string[] Values => _values.ToArray();
        public int ValueCount => _values.Count;
        
        public void RespondDisplayResult(string[] resultValues, string title)
        {
            ResponseType = ResponseType.DisplayResult;
            Title = title;
            _values.Clear();
            _values.AddRange(resultValues);
            OnResponse?.Invoke();
        }
        
        public void RespondDisplayRandomizable(int randomizableId)
        {
            var randomizable = _randomizableGateway.GetById(randomizableId);
         
            ResponseType = ResponseType.DisplayRandomizable;   
            Title = randomizable.Name;
            
            _values.Clear();
            foreach (var item in randomizable.Items)
            {
                _values.Add(item.Name);
            }
            
            OnResponse?.Invoke();
        }

        public void RespondDisplayLabel(int labelId)
        {
            ResponseType = ResponseType.DisplayLabel;
            Title = "";
            _values.Clear();
            
            var randomizables = _randomizableGateway.GetAll();
            if (labelId >= 0)
            {
                var label = _labelGateway.GetById(labelId);
                Title = label.Name;
            }
            
            foreach (var randomizable in randomizables)
            {
                if (randomizable.HasLabel(labelId) || labelId == -1)
                    _values.Add(randomizable.Name);
            }
            
            OnResponse?.Invoke();
        }
    }
}