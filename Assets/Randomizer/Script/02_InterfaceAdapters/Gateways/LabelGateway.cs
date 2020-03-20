using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class LabelGateway : IGateway<Label>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        public int ActiveId
        {
            get => _dataStore.ActiveId;
            set => _dataStore.ActiveId = value;
        }
        public Label Active => _labels[ActiveId];
        
        private readonly IDataStore<LabelData> _dataStore;
        private readonly IDictionary<int, Label> _labels = new Dictionary<int, Label>();

        public LabelGateway(IDataStore<LabelData> dataStore)
        {
            _dataStore = dataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            foreach (var data in _dataStore.Data)
            {
                var label = new Label(data.id, data.name);
                _labels.Add(data.id, label);
            }
        }

        public Label this[int index] => _labels.ElementAt(index).Value;
        public Label[] GetAll() => _labels.Values.ToArray();
        public Label GetById(int id) => _labels[id];
        public int GetIndex(int id) => _labels.Keys.ToList().IndexOf(id);

        public void Save(int id)
        {
            var label = _labels[id];
            var data = _dataStore[id];
                data.name = label.Name;
        }
        
        public void AddNew(Label newInstance)
        {
            var id = newInstance.Id;
            _labels.Add(id, newInstance);
            _dataStore.Create(id);
            Save(id);
        }
        
        public void Remove(int id)
        {
            _labels.Remove(id);
            _dataStore.Delete(id);
        }
    }
}