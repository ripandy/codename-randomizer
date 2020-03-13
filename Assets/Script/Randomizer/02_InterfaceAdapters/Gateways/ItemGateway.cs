using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class ItemGateway : IGateway<Item>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        public int ActiveId
        {
            get => _dataStore.ActiveId;
            set => _dataStore.ActiveId = value;
        }
        public Item Active => _items[ActiveId];
        
        private readonly IDataStore<ItemData> _dataStore;
        private readonly IDictionary<int, Item> _items = new Dictionary<int, Item>();

        public ItemGateway(IDataStore<ItemData> dataStore)
        {
            _dataStore = dataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            foreach (var data in _dataStore.Data)
            {
                var item = new Item(data.id, data.name);
                _items.Add(data.id, item);
            }
        }
        
        public Item[] GetAll() => _items.Values.ToArray();
        public Item GetById(int id) => _items[id];

        public void Save(int id)
        {
            var item = _items[id];
            var data = _dataStore[id];
            data.name = item.Name;
        }
        
        public void AddNew(Item newInstance)
        {
            var id = newInstance.Id;
            _items.Add(id, newInstance);
            _dataStore.Create(id);
            Save(id);
        }
        
        public void Remove(int id)
        {
            _items.Remove(id);
            _dataStore.Delete(id);
        }
    }
}