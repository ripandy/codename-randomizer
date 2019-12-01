using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class RandomizableDataStore : IDataStore<RandomizableData>
    {
        [SerializeField] private RandomizableData[] randomizableData;
        public RandomizableData this[int index] => randomizableData[index];
        public RandomizableData[] Data => randomizableData;
    }
}