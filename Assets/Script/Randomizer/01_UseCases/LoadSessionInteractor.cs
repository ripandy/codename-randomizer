using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class LoadSessionInteractor : IInputPortInteractor<int>
    {
        private readonly Session _session;
        private readonly IGateway<Group> _groupGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action<int> InputHandler => Handle;
        
        private const int Data = 0;
        Action IInputPortInteractor.InputHandler => () => Handle(Data);

        private LoadSessionInteractor(
            Session session,
            IGateway<Group> groupGateway,
            IGateway<Randomizable> randomizableGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _groupGateway = groupGateway;
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
                RespondAsGroup();
            }
            else
            {
                RespondAsRandomizable();
            }
            _responseInteractor.RaiseResponseEvent();
        }

        private void RespondAsGroup()
        {
            _responseInteractor.ResponseType = ResponseType.DisplayGroup;
            var groupId = _session.ActiveGroupId;
            var randomizables = _randomizableGateway.GetAll();
            if (groupId != -1)
            {
                var group = _groupGateway.GetById(groupId);
                _responseInteractor.Title = group.Name;
                foreach (var rId in group.RandomizeableIds)
                {
                    _responseInteractor.AddValue(randomizables[rId].Name);
                }
            }
            else
            {
                foreach (var randomizable in randomizables)
                {
                    _responseInteractor.AddValue(randomizable.Name);
                }
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