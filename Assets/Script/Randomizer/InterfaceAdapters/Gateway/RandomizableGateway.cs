using System.Collections.Generic;
using Randomizer.Entity;

namespace Randomizer.InterfaceAdapters.Gateway
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

        private void Initialize()
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
            return _randomizables[id];
        }
    }
}