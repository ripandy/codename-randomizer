using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class LabelDataStore : IDataStore<LabelData>
    {
        [SerializeField] private int activeId;
        [SerializeField] private List<LabelData> labelData;

#if UNITY_ANDROID
        private readonly string _jsonFilename = Application.persistentDataPath + "/LabelData.json";        
#else
        private readonly string _jsonFilename = Application.dataPath + "/LabelData.json";
#endif
        
        public int ActiveId
        {
            get => activeId;
            set => activeId = value;
        }

        public LabelData this[int id] => labelData.First(data => data.id == id);
        public LabelData[] Data => labelData.ToArray();

        public void Create(int id)
        {
            labelData.Add(new LabelData {id = id});
        }

        public void Delete(int id)
        {
            labelData.Remove(this[id]);
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