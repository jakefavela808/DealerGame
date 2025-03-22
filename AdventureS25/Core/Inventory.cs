using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents an inventory that can hold items
    /// </summary>
    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        /// <summary>
        /// Add an item to the inventory
        /// </summary>
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Remove an item from the inventory
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
        /// Check if the inventory contains a specific item
        /// </summary>
        public bool HasItem(string itemName)
        {
            return items.Any(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get all items in the inventory
        /// </summary>
        public List<Item> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Get a string representation of the inventory
        /// </summary>
        public string GetInventoryString()
        {
            if (items.Count == 0)
            {
                return "Your inventory is empty.";
            }
            
            string result = "Inventory:";
            foreach (var item in items)
            {
                result += $"\n- {item}";
            }
            
            return result;
        }
    }
}
