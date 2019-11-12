using System.Collections.Generic;
using Randomizer.Entity;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class RandomizableGateway : IGateway<Randomizable>
    {
        private readonly IDataStore<RandomizableData> _cachedDataStore;
        private readonly IList<Randomizable> _randomizables = new List<Randomizable>();

        public Randomizable this[int index] => _randomizables[index];

        private RandomizableGateway(IDataStore<RandomizableData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
            Initialize();
        }

        public void Initialize()
        {
            foreach (var data in _cachedDataStore.Data)
            {
                var randomizable = new Randomizable();
                foreach (var item in data.items)
                {
                    randomizable.AddItem(new Item { Name = item });
                }
                _randomizables.Add(randomizable);
            }
        }
        
        public Randomizable GetById(int id)
        {
            return this[id];
        }
    }
}