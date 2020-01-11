using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
    {
        private readonly IGateway<Label> _labelGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private LoadSessionInteractor(
            IGateway<Label> labelGateway,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _labelGateway = labelGateway;
            _randomizableGateway = randomizableGateway;
            _responseInteractor = responseInteractor;
        }
        
        private void Handle(int request)
        {
            _randomizableGateway.ActiveId = request;
            
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
            _responseInteractor.ResponseType = ResponseType.DisplayLabel;
            var labelId = _labelGateway.ActiveId;
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
            var randomizable = _randomizableGateway.GetActive();
                
            _responseInteractor.ResponseType = ResponseType.DisplayRandomizable;
            _responseInteractor.Title = randomizable.Name;
            foreach (var item in randomizable.Items)
            {
                _responseInteractor.AddValue(item.Name);
            }
        }
    }
}