using System;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents an item in the game world
    /// </summary>
    public class Item
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }

        public Item(string id, string name, string description, int value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }

        /// <summary>
        /// Get a string representation of the item
        /// </summary>
        public override string ToString()
        {
            return $"{Name} ({Value} gold)";
        }

        /// <summary>
        /// Get a detailed description of the item
        /// </summary>
        public string GetDetailedDescription()
        {
            return $"{Name}: {Description} Worth {Value} gold.";
        }
    }
}
