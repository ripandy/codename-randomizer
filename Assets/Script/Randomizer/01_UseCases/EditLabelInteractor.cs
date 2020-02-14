using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class EditLabelInteractor : BaseInteractor
    {
        public EditLabelInteractor(
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway)
            : base(requestInteractor, responseInteractor, labelGateway, randomizableGateway)
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