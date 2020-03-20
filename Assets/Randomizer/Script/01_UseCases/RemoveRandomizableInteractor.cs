using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveRandomizableInteractor : BaseInteractor
    {
        public RemoveRandomizableInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.RemoveRandomizable) return;
            
            RandomizableGateway.Remove(RandomizableGateway.ActiveId);
            RandomizableGateway.ActiveId = -1;

            var labelId = LabelGateway.ActiveId;
            var title = "";
            
            var randomizables = RandomizableGateway.GetAll()
                .Select(randomizable => randomizable)
                .Where(randomizable => randomizable.HasLabel(labelId) || labelId == -1);
            
            if (labelId >= 0)
            {
                title = LabelGateway.GetById(labelId).Name;
            }
            
            var items = randomizables.Select(randomizable => randomizable.Name).ToArray();

            ResponseInteractor.Response(new LabelResponseMessage(title, items));
        }
    }
}