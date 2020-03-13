using System;
using System.Collections.Generic;
using System.Linq;

namespace Randomizer.Entities
{
    public class Randomizable
    {
        private readonly IList<int> _itemIds = new List<int>();
        private readonly IList<int> _labelIds = new List<int>();

        private static int _idCounter;
        
        public int Id { get; }
        public string Name { get; set; }

        public Randomizable()
        {
            Id = _idCounter++;
        }

        public Randomizable(int id, string name)
        {
            Id = id;
            Name = name;
            _idCounter = Math.Max(_idCounter, id) + 1;
        }
        
        public int ItemCount => _itemIds.Count;
        public int[] ItemIds => _itemIds.ToArray();
        public int LabelCount => _labelIds.Count;
        public int[] LabelIds => _labelIds.ToArray();

        public void AddItem(int itemId) => _itemIds.Add(itemId);
        public void RemoveItem(int itemId) => _itemIds.Remove(itemId);
        public bool HasItem(int itemId) => _itemIds.Contains(itemId);

        public void AddLabel(int labelId) => _labelIds.Add(labelId);
        public void RemoveLabel(int labelId) => _labelIds.Remove(labelId);
        public bool HasLabel(int labelId) => _labelIds.Contains(labelId);

        public int Randomize()
        {
            var rnd = new Random();
            var rIndex = rnd.Next(ItemCount);
            return _itemIds[rIndex];
        }
    }
}
