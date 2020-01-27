using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadLabelInteractor : BaseInteractor
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;

        private LoadLabelInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IRequestInteractor requestInteractor,
            IResponseInteractor responseInteractor)
            : base(requestInteractor, responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
        }
        
        protected override void OnRequest(LoadLabelRequestMessage requestMessage)
        {
            if (requestMessage.RequestType != RequestType.LoadLabel) return;
            
            if (!requestMessage.LoadActive)
                _labelGateway.ActiveId = requestMessage.LabelId;
            if (_randomizableGateway.ActiveId >= 0)
            {
                var randomizable = _randomizableGateway.GetActive();
                if (string.IsNullOrEmpty(randomizable.Name) && randomizable.ItemCount == 0)
                    _randomizableGateway.Remove(_randomizableGateway.ActiveId);
                _randomizableGateway.ActiveId = -1;
            }
            
            var labelId = _labelGateway.ActiveId;
            var title = "";
            
            var randomizables = _randomizableGateway.GetAll()
                .Select(randomizable => randomizable)
                .Where(randomizable => randomizable.HasLabel(labelId) || labelId == -1);
            
            if (labelId >= 0)
            {
                title = _labelGateway.GetById(labelId).Name;
            }
            
            var items = randomizables.Select(randomizable => randomizable.Name).ToArray();

            ResponseInteractor.Response(new LabelResponseMessage(title, items));
        }
    }
}