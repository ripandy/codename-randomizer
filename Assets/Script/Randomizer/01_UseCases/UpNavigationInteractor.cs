using System;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class UpNavigationInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IGateway<Group> _groupGateway;
        private readonly IOutputPortInteractor _responseInteractor;
        
        public Action InputHandler => Handle;

        private UpNavigationInteractor(
            Session session,
            IGateway<Randomizable> randomizableGateway,
            IGateway<Group> groupGateway,
            IOutputPortInteractor responseInteractor)
        {
            _session = session;
            _randomizableGateway = randomizableGateway;
            _groupGateway = groupGateway;
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
                RespondAsGroup();

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

        private void RespondAsGroup()
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
    }
}