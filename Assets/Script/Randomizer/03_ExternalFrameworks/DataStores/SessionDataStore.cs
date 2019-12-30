using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class SessionDataStore : IDataStore<UserPreferencesData>
    {
        [SerializeField] private List<UserPreferencesData> userData;
        public UserPreferencesData this[int index] => userData[index];
        public UserPreferencesData[] Data => userData.ToArray();
        
        public int Create()
        {
            userData.Add(new UserPreferencesData());
            return userData.Count - 1;
        }

        public void Delete(int index)
        {
            userData.RemoveAt(index);
        }
    }
}