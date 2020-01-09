using System;
using System.Collections.Generic;
using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.DataStores
{
    [Serializable]
    public class LabelDataStore : IDataStore<LabelData>
    {
        [SerializeField] private int activeIndex;
        [SerializeField] private List<LabelData> labelData;

        public int ActiveIndex
        {
            get => activeIndex;
            set => activeIndex = value;
        }

        public LabelData this[int index] => labelData[index];
        public LabelData[] Data => labelData.ToArray();
        public LabelData ActiveData => labelData[activeIndex];

        public int Create()
        {
            labelData.Add(new LabelData());
            return labelData.Count - 1;
        }

        public void Delete(int index)
        {
            labelData.RemoveAt(index);
        }
    }
}