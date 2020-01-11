using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private RandomizeInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle()
        {
            _responseInteractor.ResponseType = ResponseType.DisplayResult;
            _responseInteractor.ClearValue();

            var randomizableId = _randomizableGateway.ActiveId;
            var labelId = _labelGateway.ActiveId;
            var randomizables = _randomizableGateway.GetAll();
            if (randomizableId >= 0)
            {
                randomizables = new [] { _randomizableGateway.GetById(randomizableId) };
                _responseInteractor.Title = randomizables[0].Name;
            }
            else
            {
                if (labelId >= 0)
                {
                    var label = _labelGateway.GetById(labelId);
                    _responseInteractor.Title = label.Name;
                    randomizables = _randomizableGateway.GetAll().Select(randomizable => randomizable)
                        .Where(randomizable => randomizable.HasLabel(labelId))
                        .ToArray();
                }
            }
            
            foreach (var randomizable in randomizables)
            {
                if (randomizable.ItemCount > 0)
                {
                    var result = randomizable.Randomize();
                    _responseInteractor.AddValue(result.Name);
                }
            }

            if (_responseInteractor.ValueCount > 0)
                _responseInteractor.RaiseResponseEvent();
        }
    }
}