using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class GroupDataStore : IDataStore<GroupData>
    {
        [SerializeField] private List<GroupData> groupData;
        public GroupData this[int index] => groupData[index];
        public GroupData[] Data => groupData.ToArray();
        
        public int Create()
        {
            groupData.Add(new GroupData());
            return groupData.Count - 1;
        }

        public void Delete(int index)
        {
            groupData.RemoveAt(index);
        }
    }
}