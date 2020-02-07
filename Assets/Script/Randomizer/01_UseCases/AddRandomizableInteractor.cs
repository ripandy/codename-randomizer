using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class AddRandomizableInteractor : BaseInteractor
    {
        private AddRandomizableInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor) 
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
        {
        }

        protected override void OnRequest(RequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.AddRandomizable) return;
            
            var randomizable = new Randomizable();
            var newId = RandomizableGateway.AddNew(randomizable);
            RandomizableGateway.ActiveId = newId;
            
            RespondRandomizable(randomizable);
        }
    }
}