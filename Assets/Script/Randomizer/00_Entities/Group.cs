using System.Collections.Generic;
using System.Linq;

namespace Randomizer.Entities
{
    public class Group
    {
        private readonly IList<int> _randomizeableIds = new List<int>(); 
        public string Name { get; set; }
        public int[] RandomizeableIds => _randomizeableIds.ToArray();
        public int RandomizableCount => _randomizeableIds.Count;
        public void Add(int id) => _randomizeableIds.Add(id);
        public void Remove(int id) => _randomizeableIds.Remove(id);
        public void Clear() => _randomizeableIds.Clear();
    }
}