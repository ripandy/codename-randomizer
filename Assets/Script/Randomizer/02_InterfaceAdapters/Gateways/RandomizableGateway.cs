using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class RandomizableGateway : IGateway<Randomizable>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        public int ActiveId
        {
            get => _dataStore.ActiveIndex;
            set => _dataStore.ActiveIndex = value;
        }

        private readonly IDataStore<RandomizableData> _dataStore;
        private readonly IList<Randomizable> _randomizables = new List<Randomizable>();

        public int Length => _randomizables.Count;

        private RandomizableGateway(IDataStore<RandomizableData> dataStore)
        {
            _dataStore = dataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            foreach (var data in _dataStore.Data)
            {
                var randomizable = new Randomizable{ Name = data.name };
                
                foreach (var item in data.items)
                {
                    randomizable.AddItem(new Item { Name = item });
                }

                foreach (var labelId in data.labelIds)
                {
                    randomizable.AddLabel(labelId);
                }
                
                _randomizables.Add(randomizable);
            }
        }

        public Randomizable[] GetAll() => _randomizables.ToArray();
        public Randomizable GetById(int id) => id < _randomizables.Count ? _randomizables[id] : null;
        public Randomizable GetActive() => _randomizables[ActiveId];

        public void Save(int id)
        {
            if (id >= _randomizables.Count) return;
            
            var randomizable = _randomizables[id];
            var data = _dataStore[id];

            data.name = randomizable.Name;
            data.items = randomizable.Items
                .Select(item => item.Name)
                .ToArray();
            data.labelIds = randomizable.LabelIds;
        }

        public int AddNew(Randomizable newInstance)
        {
            _randomizables.Add(newInstance);
            _dataStore.Create();
            var index = _randomizables.Count - 1;
            Save(index);
            return index;
        }

        public void Remove(int id)
        {
            _randomizables.RemoveAt(id);
            _dataStore.Delete(id);
        }
    }
}