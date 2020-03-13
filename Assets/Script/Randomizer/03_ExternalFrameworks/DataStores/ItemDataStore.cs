using System;
using System.Collections.Generic;
using System.Linq;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class ItemDataStore : IDataStore<ItemData>
    {
        [SerializeField] private int activeId;
        [SerializeField] private List<ItemData> itemData;

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
    }
}