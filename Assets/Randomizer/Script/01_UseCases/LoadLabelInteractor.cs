using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadLabelInteractor : BaseInteractor
    {
        private LoadLabelInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }
        
        protected override void OnRequest(LoadLabelRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.LoadLabel) return;
            
            if (!requestMessage.LoadActive)
                LabelGateway.ActiveId = requestMessage.LabelIndex >= 0 ? LabelGateway[requestMessage.LabelIndex].Id : -1;
            if (RandomizableGateway.ActiveId >= 0)
            {
                var randomizable = RandomizableGateway.Active;
                if (string.IsNullOrEmpty(randomizable.Name) && randomizable.ItemCount == 0)
                    RandomizableGateway.Remove(RandomizableGateway.ActiveId);
                RandomizableGateway.ActiveId = -1;
            }
            
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