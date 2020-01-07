using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class GroupGateway : IGateway<Group>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IDataStore<GroupData> _cachedDataStore;
        private readonly IList<Group> _groups = new List<Group>();
        
        public int Length => _groups.Count;

        public GroupGateway(IDataStore<GroupData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
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

            data.name = group.Name;
            data.randomizableIds = group.RandomizeableIds;
        }

        public int AddNew(Group newInstance)
        {
            _groups.Add(newInstance);
            _cachedDataStore.Create();
            var index = _groups.Count - 1;
            Save(index);
            return index;
        }
        
        public void Remove(int id)
        {
            _groups.RemoveAt(id);
            _cachedDataStore.Delete(id);
        }
    }
}