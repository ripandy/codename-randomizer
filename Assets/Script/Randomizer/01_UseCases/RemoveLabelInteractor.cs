using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveLabelInteractor : BaseInteractor
    {
        private RemoveLabelInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            var id = requestMessage.Value;
            if (requestMessage.RequestType != RequestType.RemoveLabel || id < 0) return;
            
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