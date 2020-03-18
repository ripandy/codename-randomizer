using System;

namespace Randomizer.Entities
{
    public class Item
    {
        private static int _idCounter;
        public int Id { get; }
        public string Name { get; set; }

        public Item(string name)
        {
            Id = _idCounter++;
            Name = name;
        }
        
        public Item(int id, string name)
        {
            Id = id;
            Name = name;
            _idCounter = Math.Max(_idCounter, id) + 1;
        }
    }
}
