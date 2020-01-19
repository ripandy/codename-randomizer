using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        
        private AddRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor) 
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddRandomizable) return;
            
            var randomizable = new Randomizable();
            var newId = _randomizableGateway.AddNew(randomizable);
            _randomizableGateway.ActiveId = newId;
            
            RespondRandomizable(randomizable);
        }
    }
}