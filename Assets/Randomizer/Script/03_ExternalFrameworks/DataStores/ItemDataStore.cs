using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class ItemDataStore : IDataStore<ItemData>, IDisposable
    {
        [SerializeField] private int activeId;
        [SerializeField] private List<ItemData> itemData;

        private const string JsonFilename = "ItemData.json";        
        
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
            ExternalJsonHandler.SaveToJson(this, JsonFilename);
        }

        public void LoadFromJson()
        {
            if (ExternalJsonHandler.IsJsonExist(JsonFilename))
                ExternalJsonHandler.LoadFromJson(this, JsonFilename);
            else
                SaveToJson();
        }

        public void Dispose()
        {
            SaveToJson();
        }
    }
}