using System;

namespace Randomizer.Entities
{
    public class Label
    {
        private static int _idCounter;
        public int Id { get; }
        public string Name { get; set; }
        
        public Label(string name)
        {
            Id = _idCounter++;
            Name = name;
        }
        
        public Label(int id, string name)
        {
            Id = id;
            Name = name;
            _idCounter = Math.Max(_idCounter, id) + 1;
        }
    }
}