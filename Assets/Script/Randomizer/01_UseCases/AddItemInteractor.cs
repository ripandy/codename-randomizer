using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddItemInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;

        protected AddItemInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor) 
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddItem || string.IsNullOrEmpty(requestMessage.Value)) return;

            var id = _randomizableGateway.ActiveId;
            var randomizable = _randomizableGateway.GetById(id);
            var newItem = new Item { Name = requestMessage.Value };
            randomizable.AddItem(newItem);
            _randomizableGateway.Save(id);
            
            RespondRandomizable(randomizable);
        }
    }
}