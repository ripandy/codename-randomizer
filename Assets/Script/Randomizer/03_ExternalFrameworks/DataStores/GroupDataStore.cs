using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class GroupDataStore : IDataStore<GroupData>
    {
        [SerializeField] private GroupData[] groupData;
        public GroupData this[int index] => groupData[index];
        public GroupData[] Data => groupData;
    }
}