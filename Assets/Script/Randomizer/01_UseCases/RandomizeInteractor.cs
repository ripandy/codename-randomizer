using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IResponseInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private RandomizeInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IResponseInteractor responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle()
        {
            var randomizableId = _randomizableGateway.ActiveId;
            var labelId = _labelGateway.ActiveId;
            var randomizables = _randomizableGateway.GetAll();
            var title = "";
            if (randomizableId >= 0)
            {
                randomizables = new [] { _randomizableGateway.GetById(randomizableId) };
                title = randomizables[0].Name;
            }
            else
            {
                if (labelId >= 0)
                {
                    var label = _labelGateway.GetById(labelId);
                    title = label.Name;
                    randomizables = _randomizableGateway.GetAll().Select(randomizable => randomizable)
                        .Where(randomizable => randomizable.HasLabel(labelId))
                        .ToArray();
                }
            }

            var results = randomizables.Select(randomizable => randomizable.Randomize().Name).ToArray();
            if (results.Any())
                _responseInteractor.RespondDisplayResult(results, title);
        }
    }
}