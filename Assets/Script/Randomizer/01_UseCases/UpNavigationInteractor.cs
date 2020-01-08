using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class UpNavigationInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IGateway<Label> _labelGateway;
        private readonly IOutputPortInteractor _responseInteractor;
        
        public Action InputHandler => Handle;

        private UpNavigationInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IGateway<Label> labelGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _labelGateway = labelGateway;
            _responseInteractor = responseInteractor;
        }

        private void Handle()
        {
            _responseInteractor.Title = "";
            _responseInteractor.ClearValue();
            
            var currentState = _responseInteractor.ResponseType;
            if (_session.ActiveRandomizableId >= 0 && currentState == ResponseType.DisplayResult)
                RespondAsRandomizable();
            else
                RespondShowLabel();

            _responseInteractor.RaiseResponseEvent();
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

        private void RespondShowLabel()
        {
            if (_session.ActiveRandomizableId >= 0)
            {
                var id = _session.ActiveRandomizableId;
                var randomizable = _randomizableGateway.GetById(id);
                if (string.IsNullOrEmpty(randomizable.Name) && randomizable.ItemCount == 0)
                    _randomizableGateway.Remove(id);
            }
            
            _session.ActiveRandomizableId = -1;
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
    }
}