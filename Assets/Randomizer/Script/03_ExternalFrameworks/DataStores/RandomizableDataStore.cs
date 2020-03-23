using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class RandomizableDataStore : IDataStore<RandomizableData>
    {
        [SerializeField] private int activeId;
        [SerializeField] private List<RandomizableData> randomizableData;
        
        private const string JsonFilename = "RandomizableData.json";
        
        public int ActiveId
        {
            get => activeId;
            set => activeId = value;
        }

        public RandomizableData this[int id] => randomizableData.First(data => data.id == id);
        public RandomizableData[] Data => randomizableData.ToArray();

        public void Create(int id)
        {
            randomizableData.Add(new RandomizableData {id = id});
        }

        public void Delete(int id)
        {
            randomizableData.Remove(this[id]);
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
    }
}