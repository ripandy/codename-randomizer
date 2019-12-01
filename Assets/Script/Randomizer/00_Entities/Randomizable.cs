using System;
using System.Collections.Generic;

namespace Randomizer.Entities
{
    public class Randomizable
    {
        private readonly IList<Item> _items = new List<Item>();
        public string Name { get; set; }
        public int ItemCount => _items.Count;
        public IList<Item> Items => _items;

        public void AddItem(Item newItem) => _items.Add(newItem);
        public void Clear() => _items.Clear();

        public Item Randomize()
        {
            var rnd = new Random();
            var rIndex = rnd.Next(ItemCount);
            return _items[rIndex];
        }
    }
}