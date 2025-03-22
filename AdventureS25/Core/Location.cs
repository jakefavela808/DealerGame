using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents a location in the game world
    /// </summary>
    public class Location
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        private Dictionary<string, Location> exits;
        private List<Item> items;

        public Location(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            exits = new Dictionary<string, Location>();
            items = new List<Item>();
        }

        /// <summary>
        /// Add an exit to another location
        /// </summary>
        public void AddExit(string direction, Location destination)
        {
            exits[direction.ToLower()] = destination;
        }

        /// <summary>
        /// Get the location in a specific direction
        /// </summary>
        public Location? GetExit(string direction)
        {
            direction = direction.ToLower();
            if (exits.ContainsKey(direction))
            {
                return exits[direction];
            }
            return null;
        }

        /// <summary>
        /// Get all available exits
        /// </summary>
        public Dictionary<string, Location> GetExits()
        {
            return exits;
        }

        /// <summary>
        /// Add an item to this location
        /// </summary>
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Remove an item from this location
        /// </summary>
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Get an item by name
        /// </summary>
        public Item? GetItem(string itemName)
        {
            return items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get all items at this location
        /// </summary>
        public List<Item> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Get a full description of the location, including exits and items
        /// </summary>
        public string GetFullDescription()
        {
            string description = $"{Name}\n{Description}\n";
            
            // Add exits
            if (exits.Count > 0)
            {
                description += "\nExits:";
                foreach (var exit in exits)
                {
                    description += $" {exit.Key} ({exit.Value.Name})";
                }
            }
            
            // Add items
            if (items.Count > 0)
            {
                description += "\n\nYou can see:";
                foreach (var item in items)
                {
                    description += $"\n- {item.Name}";
                }
            }
            
            return description;
        }
    }
}
