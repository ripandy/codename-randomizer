using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class RandomizableGateway : IGateway<Randomizable>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IDataStore<RandomizableData> _cachedDataStore;
        private readonly IList<Randomizable> _randomizables = new List<Randomizable>();

        private RandomizableGateway(IDataStore<RandomizableData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            foreach (var data in _cachedDataStore.Data)
            {
                var randomizable = new Randomizable{ Name = data.name };
                foreach (var item in data.items)
                {
                    randomizable.AddItem(new Item { Name = item });
                }
                _randomizables.Add(randomizable);
            }
        }

        public Randomizable[] GetAll() => _randomizables.ToArray();

        public Randomizable GetById(int id) => id < _randomizables.Count ? _randomizables[id] : null;

        public void Save(int id)
        {
            if (id >= _randomizables.Count) return;
            
            var randomizable = _randomizables[id];
            var data = _cachedDataStore[id];

            data.name = randomizable.Name;
            data.items = randomizable.Items
                .Select(item => item.Name)
                .ToArray();
        }

        public int AddNew(Randomizable newInstance)
        {
            _randomizables.Add(newInstance);
            _cachedDataStore.Create();
            var index = _randomizables.Count - 1;
            Save(index);
            return index;
        }
    }
}