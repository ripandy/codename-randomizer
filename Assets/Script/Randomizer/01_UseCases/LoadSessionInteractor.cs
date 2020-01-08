using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
    {
        private readonly Session _session;
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private LoadSessionInteractor(
            Session session,
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle(int request)
        {
            _session.ActiveRandomizableId = request;
            
            _responseInteractor.Title = "";
            _responseInteractor.ClearValue();
            if (request == -1)
            {
                RespondShowLabel();
            }
            else
            {
                RespondAsRandomizable();
            }
            _responseInteractor.RaiseResponseEvent();
        }

        private void RespondShowLabel()
        {
            _responseInteractor.ResponseType = ResponseType.DisplayGroup;
            var labelId = _session.ActiveLabelId;
            var randomizables = _randomizableGateway.GetAll();
            if (labelId >= 0)
            {
                var label = _labelGateway.GetById(labelId);
                _responseInteractor.Title = label.Name;
            }
            
            foreach (var randomizable in randomizables)
            {
                if (randomizable.HasLabel(labelId) || labelId == -1)
                    _responseInteractor.AddValue(randomizable.Name);
            }
        }

        private void RespondAsRandomizable()
        {
            var randomizableId = _session.ActiveRandomizableId;
            var randomizable = _randomizableGateway.GetById(randomizableId);
                
            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            foreach (var item in randomizable.Items)
            {
                _responseInteractor.AddValue(item.Name);
            }
        }
    }
}