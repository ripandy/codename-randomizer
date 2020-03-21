using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class ItemDataStore : IDataStore<ItemData>
    {
        [SerializeField] private int activeId;
        [SerializeField] private List<ItemData> itemData;

#if UNITY_ANDROID
        private readonly string _jsonFilename = Application.persistentDataPath + "/ItemData.json";        
#else
        private readonly string _jsonFilename = Application.dataPath + "/ItemData.json";
#endif
        
        public int ActiveId
        {
            get => activeId;
            set => activeId = value;
        }

        public ItemData this[int id] => itemData.First(data => data.id == id);
        public ItemData[] Data => itemData.ToArray();

        public void Create(int id)
        {
            itemData.Add(new ItemData {id = id});
        }

        public void Delete(int id)
        {
            itemData.Remove(this[id]);
        }

        public void SaveToJson()
        {
            ExternalJsonHandler.SaveToJson(this, _jsonFilename);
        }

        public void LoadFromJson()
        {
            if (ExternalJsonHandler.IsJsonExist(_jsonFilename))
                ExternalJsonHandler.LoadFromJson(this, _jsonFilename);
        }
    }
}