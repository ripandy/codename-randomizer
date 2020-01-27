using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : BaseInteractor
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;

        private RandomizeInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
        }
        
        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.Randomize) return;
            
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

            var filtered = randomizables.Select(randomizable => randomizable)
                .Where(randomizable => randomizable.ItemCount > 1)
                .ToArray();

            var results = filtered.Select(randomizable => randomizable.Randomize().Name).ToArray();
            if (results.Any())
                ResponseInteractor.Response(new ResultResponseMessage(title, results));
        }
    }
}