using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class RandomizableDataStore : IDataStore<RandomizableData>
    {
        [SerializeField] private List<RandomizableData> randomizableData;
        public RandomizableData this[int index] => randomizableData[index];
        public RandomizableData[] Data => randomizableData.ToArray();
        
        public int Create()
        {
            randomizableData.Add(new RandomizableData());
            return randomizableData.Count - 1;
        }

        public void Delete(int index)
        {
            randomizableData.RemoveAt(index);
        }
    }
}