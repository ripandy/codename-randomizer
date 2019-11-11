using System;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks
{
    [Serializable]
    public class RandomizableDataStore : IDataStore<RandomizableData>
    {
        [SerializeField] private RandomizableData[] randomizableData;
        public RandomizableData[] Data => randomizableData;
    }
}