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
            get => _dataStore.ActiveId;
            set => _dataStore.ActiveId = value;
        }

        public Randomizable Active => _randomizables[ActiveId];

        private readonly IDataStore<RandomizableData> _dataStore;
        private readonly IDictionary<int, Randomizable> _randomizables = new Dictionary<int, Randomizable>();

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
                var randomizable = new Randomizable(data.id, data.name);
                
                foreach (var itemId in data.itemIds)
                {
                    randomizable.AddItem(itemId);
                }

                foreach (var labelId in data.labelIds)
                {
                    randomizable.AddLabel(labelId);
                }
                
                _randomizables.Add(data.id, randomizable);
            }
        }

        public Randomizable this[int index] => _randomizables.ElementAt(index).Value;
        public Randomizable[] GetAll() => _randomizables.Values.ToArray();
        public Randomizable GetById(int id) => _randomizables[id];
        public int GetIndex(int id) => _randomizables.Keys.ToList().IndexOf(id);

        public void Save(int id)
        {
            var randomizable = _randomizables[id];
            var data = _dataStore[id];

            data.name = randomizable.Name;
            data.itemIds = randomizable.ItemIds;
            data.labelIds = randomizable.LabelIds;
            
            _dataStore.SaveToJson();
        }

        public void AddNew(Randomizable newInstance)
        {
            var id = newInstance.Id;
            _randomizables.Add(id, newInstance);
            _dataStore.Create(id);
            Save(id);
        }

        public void Remove(int id)
        {
            _randomizables.Remove(id);
            _dataStore.Delete(id);
        }
    }
}