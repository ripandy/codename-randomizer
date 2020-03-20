using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveLabelInteractor : BaseInteractor
    {
        private RemoveLabelInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.RemoveLabel || requestMessage.Value < 0) return;
            
            var id = LabelGateway[requestMessage.Value].Id;
            var randomizables = RandomizableGateway.GetAll()
                .Select(randomizable => randomizable)
                .Where(randomizable => randomizable.HasLabel(id));
            
            foreach (var randomizable in randomizables)
            {
                randomizable.RemoveLabel(id);
            }
            
            LabelGateway.Remove(id);
            RespondManageLabel();
        }
    }
}