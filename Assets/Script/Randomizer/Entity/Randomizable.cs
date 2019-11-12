using System;
using System.Collections.Generic;

namespace Randomizer.Entity
{
    public class Randomizable
    {
        private readonly IList<Item> _items = new List<Item>();
        public int ItemCount => _items.Count;
        public IList<Item> Items => _items;

        public void AddItem(Item newItem)
        {
            _items.Add(newItem);
        }

        public Item Randomize()
        {
            var rnd = new Random();
            var rIndex = rnd.Next(ItemCount);
            return _items[rIndex];
        }
    }
}