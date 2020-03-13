using System;

namespace Randomizer.InterfaceAdapters
{
    [Serializable]
    public class RandomizableData
    {
        public int id;
        public string name;
        public int[] itemIds;
        public int[] labelIds;
    }
}