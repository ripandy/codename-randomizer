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
        
#if UNITY_ANDROID
        private readonly string _jsonFilename = Application.persistentDataPath + "/RandomizableData.json";        
#else
        private readonly string _jsonFilename = Application.dataPath + "/RandomizableData.json";
#endif
        
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
            ExternalJsonHandler.SaveToJson(this, _jsonFilename);
        }

        public void LoadFromJson()
        {
            if (ExternalJsonHandler.IsJsonExist(_jsonFilename))
                ExternalJsonHandler.LoadFromJson(this, _jsonFilename);
        }
    }
}