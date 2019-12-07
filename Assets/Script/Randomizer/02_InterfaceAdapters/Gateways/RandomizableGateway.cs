using System.Collections.Generic;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class RandomizableGateway : IGateway<Randomizable>
    {
        private readonly IDataStore<RandomizableData> _cachedDataStore;
        private readonly IList<Randomizable> _randomizables = new List<Randomizable>();

        private RandomizableGateway(IDataStore<RandomizableData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
            Initialize();
        }

        public void Initialize()
        {
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
        
        public Randomizable GetById(int id)
        {
            return id < _randomizables.Count ? _randomizables[id] : null;
        }

        public void Save(int id)
        {
            if (id >= _randomizables.Count) return;
            
            var randomizable = _randomizables[id];
            var data = _cachedDataStore[id];
            var names = new List<string>();
            
            foreach (var item in randomizable.Items)
            {
                names.Add(item.Name);
            }

            data.items = names.ToArray();
        }
    }
}