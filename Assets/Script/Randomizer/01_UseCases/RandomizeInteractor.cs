using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : BaseInteractor
    {
        private RandomizeInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }
        
        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.Randomize) return;
            
            var randomizableId = RandomizableGateway.ActiveId;
            var labelId = LabelGateway.ActiveId;
            var randomizables = RandomizableGateway.GetAll();
            var title = "";
            if (randomizableId >= 0)
            {
                randomizables = new [] { RandomizableGateway.GetById(randomizableId) };
                title = randomizables[0].Name;
            }
            else
            {
                if (labelId >= 0)
                {
                    var label = LabelGateway.GetById(labelId);
                    title = label.Name;
                    randomizables = RandomizableGateway.GetAll().Select(randomizable => randomizable)
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