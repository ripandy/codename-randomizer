using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditLabelInteractor : BaseInteractor
    {
        public EditLabelInteractor(
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IGateway<Item> itemGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor, randomizableGateway, labelGateway, itemGateway)
        {
        }
        
        protected override void OnRequest(EditLabelRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.EditLabel) return;
            
            var id = requestMessage.LabelId;
            var label = LabelGateway.GetById(id);
                label.Name = requestMessage.NewLabelName;
            LabelGateway.Save(id);
            
            RespondManageLabel();
        }
    }
}