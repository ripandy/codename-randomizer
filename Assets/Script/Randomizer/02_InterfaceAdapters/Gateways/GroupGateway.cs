using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class GroupGateway : IGateway<Group>, IInitializable
    {
        private readonly IDataStore<GroupData> _cachedDataStore;
        private readonly IList<Group> _groups = new List<Group>();

        public GroupGateway(IDataStore<GroupData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
        }

        public void Initialize()
        {
            foreach (var data in _cachedDataStore.Data)
            {
                var group = new Group { Name = data.name };
                foreach (var id in data.randomizableIds)
                {
                    group.Add(id);
                }
                _groups.Add(group);
            }
        }
        
        public Group[] GetAll() => _groups.ToArray();
        public Group GetById(int id) => id < _groups.Count ? _groups[id] : null;

        public void Save(int id)
        {
            if (id >= _groups.Count) return;
            
            var group = _groups[id];
            var data = _cachedDataStore[id];
            var ids = new List<int>();
            
            foreach (var randomizableId in group.RandomizeableIds)
            {
                ids.Add(randomizableId);
            }

            data.randomizableIds = ids.ToArray();
        }
    }
}