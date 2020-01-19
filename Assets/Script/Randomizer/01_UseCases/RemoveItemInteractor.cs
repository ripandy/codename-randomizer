using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RemoveItemInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        
        private RemoveItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(RequestMessage<int> requestMessage)
        {
            var id = _randomizableGateway.ActiveId;
            if (requestMessage.RequestType != RequestType.RemoveItem || id < 0) return;
            
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.RemoveItem(requestMessage.Value);
            _randomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}