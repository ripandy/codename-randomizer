using System;

namespace Randomizer.InterfaceAdapters
{
    [Serializable]
    public class RandomizableData
    {
        public string name;
        public string[] items;
        public int[] labelIds;
    }
}