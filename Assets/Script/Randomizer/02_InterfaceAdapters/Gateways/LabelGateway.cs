using System.Collections.Generic;
using System.Linq;
using Randomizer.Entities;

namespace Randomizer.InterfaceAdapters.Gateways
{
    public class LabelGateway : IGateway<Label>, IInitializable
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IDataStore<LabelData> _cachedDataStore;
        private readonly IList<Label> _labels = new List<Label>();
        
        public int Length => _labels.Count;

        public LabelGateway(IDataStore<LabelData> cachedDataStore)
        {
            _cachedDataStore = cachedDataStore;
        }

        public void Initialize()
        {
            if (IsInitialized) return;
                IsInitialized = true;
            foreach (var data in _cachedDataStore.Data)
            {
                var label = new Label { Name = data.name };
                _labels.Add(label);
            }
        }
        
        public Label[] GetAll() => _labels.ToArray();
        public Label GetById(int id) => id < _labels.Count ? _labels[id] : null;

        public void Save(int id)
        {
            if (id >= _labels.Count) return;
            
            var label = _labels[id];
            var data = _cachedDataStore[id];
                data.name = label.Name;
        }

        public int AddNew(Label newInstance)
        {
            _labels.Add(newInstance);
            _cachedDataStore.Create();
            var index = _labels.Count - 1;
            Save(index);
            return index;
        }
        
        public void Remove(int id)
        {
            _labels.RemoveAt(id);
            _cachedDataStore.Delete(id);
        }
    }
}