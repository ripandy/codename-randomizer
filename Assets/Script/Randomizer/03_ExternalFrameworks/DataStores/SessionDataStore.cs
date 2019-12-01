using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class SessionDataStore : IDataStore<UserPreferencesData>
    {
        [SerializeField] private UserPreferencesData[] userData;
        public UserPreferencesData[] Data => userData;
        public UserPreferencesData this[int index] => userData[index];
    }
}