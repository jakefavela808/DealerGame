using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Static class that manages all locations and characters in the game world
    /// </summary>
    public static class World
    {
        private static Dictionary<string, Location> locations = new Dictionary<string, Location>();
        private static Dictionary<string, Character> characters = new Dictionary<string, Character>();
        private static Dictionary<string, Item> items = new Dictionary<string, Item>();

        /// <summary>
        /// Add a location to the world
        /// </summary>
        public static void AddLocation(Location location)
        {
            if (!locations.ContainsKey(location.Id))
            {
                locations.Add(location.Id, location);
            }
        }

        /// <summary>
        /// Get a location by its ID
        /// </summary>
        public static Location? GetLocation(string id)
        {
            if (locations.ContainsKey(id))
            {
                return locations[id];
            }
            return null;
        }

        /// <summary>
        /// Add a character to the world
        /// </summary>
        public static void AddCharacter(Character character)
        {
            if (!characters.ContainsKey(character.Id))
            {
                characters.Add(character.Id, character);
            }
        }

        /// <summary>
        /// Get a character by its ID
        /// </summary>
        public static Character? GetCharacter(string id)
        {
            if (characters.ContainsKey(id))
            {
                return characters[id];
            }
            return null;
        }

        /// <summary>
        /// Get all characters at a specific location
        /// </summary>
        public static List<Character> GetCharactersAtLocation(Location location)
        {
            return characters.Values.Where(c => c.CurrentLocation == location).ToList();
        }

        /// <summary>
        /// Add an item to the world
        /// </summary>
        public static void AddItem(Item item)
        {
            if (!items.ContainsKey(item.Id))
            {
                items.Add(item.Id, item);
            }
        }

        /// <summary>
        /// Get an item by its ID
        /// </summary>
        public static Item? GetItem(string id)
        {
            if (items.ContainsKey(id))
            {
                return items[id];
            }
            return null;
        }

        /// <summary>
        /// Reset the world (for testing or restarting)
        /// </summary>
        public static void Reset()
        {
            locations.Clear();
            characters.Clear();
            items.Clear();
        }
    }
}
