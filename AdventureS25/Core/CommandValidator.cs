using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Validates commands entered by the player
    /// </summary>
    public static class CommandValidator
    {
        private static readonly List<string> validVerbs = new List<string>
        {
            "go", "look", "take", "drop", "inventory", "talk", "help", "quit", "accept", "decline", "complete", "quests"
        };
        
        private static readonly List<string> standaloneVerbs = new List<string>
        {
            "inventory", "look", "help", "quit", "quests", "accept", "decline", "complete"
        };
        
        private static readonly List<string> directions = new List<string>
        {
            "north", "south", "east", "west", "up", "down", "n", "s", "e", "w"
        };

        /// <summary>
        /// Check if a verb is valid
        /// </summary>
        public static bool IsValidVerb(string verb)
        {
            return validVerbs.Contains(verb.ToLower());
        }

        /// <summary>
        /// Check if a verb can be used without a noun
        /// </summary>
        public static bool IsStandaloneVerb(string verb)
        {
            return standaloneVerbs.Contains(verb.ToLower());
        }

        /// <summary>
        /// Check if a string is a valid direction
        /// </summary>
        public static bool IsDirection(string direction)
        {
            return directions.Contains(direction.ToLower());
        }

        /// <summary>
        /// Get all valid verbs
        /// </summary>
        public static List<string> GetVerbs()
        {
            return validVerbs;
        }

        /// <summary>
        /// Get all valid directions
        /// </summary>
        public static List<string> GetDirections()
        {
            return directions;
        }
    }
}
