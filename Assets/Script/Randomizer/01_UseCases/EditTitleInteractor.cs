using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditTitleInteractor : BaseInteractor
    {
        private EditTitleInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }

        protected override void OnRequest(RequestMessage<string> requestMessage)
        {
            var id = RandomizableGateway.ActiveId;
            if (id <= 0 || requestMessage.RequestType != RequestType.EditTitle) return;
            
            var randomizable = RandomizableGateway.GetById(id);
                randomizable.Name = requestMessage.Value;
            RandomizableGateway.Save(id);

            RespondRandomizable(randomizable);
        }
    }
}