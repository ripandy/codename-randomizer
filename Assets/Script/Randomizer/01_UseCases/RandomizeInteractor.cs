using System;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.UseCases
{
    public class RandomizeInteractor : IInputPortInteractor
    {
        private readonly Session _session;
        private readonly IGateway<Group> _groupGateway;
        private readonly IGateway<Randomizable> _randomizableGateway;
        private readonly IOutputPortInteractor _responseInteractor;

        public Action InputHandler => Handle;

        private RandomizeInteractor(
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
        
        private void Handle()
        {
            _responseInteractor.ResponseType = ResponseType.DisplayResult;
            _responseInteractor.ClearValue();

            var randomizableId = _session.ActiveRandomizableId;
            var groupId = _session.ActiveGroupId;
            var ids = new[] { randomizableId };
            if (randomizableId >= 0)
            {
                _responseInteractor.Title = _randomizableGateway.GetById(randomizableId).Name;
            }
            else
            {
                if (groupId >= 0)
                {
                    var group = _groupGateway.GetById(groupId);
                    ids = group.RandomizeableIds;
                    _responseInteractor.Title = group.Name;
                }
                else
                {
                    ids = Enumerable.Range(0, _randomizableGateway.Length).ToArray();
                }
            }
            
            foreach (var id in ids)
            {
                var randomizable = _randomizableGateway.GetById(id);
                if (randomizable.ItemCount > 0)
                {
                    var result = randomizable.Randomize();
                    _responseInteractor.AddValue(result.Name);
                }
            }

            if (_responseInteractor.ValueCount > 0)
                _responseInteractor.RaiseResponseEvent();
        }
    }
}