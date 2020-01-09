using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class RandomizableDataStore : IDataStore<RandomizableData>
    {
        [SerializeField] private int activeIndex;
        [SerializeField] private List<RandomizableData> randomizableData;

        public int ActiveIndex
        {
            get => activeIndex;
            set => activeIndex = value;
        }

        public RandomizableData this[int index] => randomizableData[index];
        public RandomizableData[] Data => randomizableData.ToArray();
        public RandomizableData ActiveData => randomizableData[ActiveIndex];

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