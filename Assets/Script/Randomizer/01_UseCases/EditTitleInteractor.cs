using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditTitleInteractor : BaseInteractor
    {
        private readonly IGateway<Randomizable> _randomizableGateway;
        
        private EditTitleInteractor(
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            _randomizableGateway = randomizableGateway;
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            var id = _randomizableGateway.ActiveId;
            if (id <= 0 || requestMessage.RequestType != RequestType.EditTitle) return;
            
            var randomizable = _randomizableGateway.GetById(id);
                randomizable.Name = requestMessage.Value;
            _randomizableGateway.Save(id);

            RespondRandomizable(randomizable);
        }
    }
}