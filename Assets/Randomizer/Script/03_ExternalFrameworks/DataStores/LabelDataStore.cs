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

        private const string JsonFilename = "LabelData.json";
        
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