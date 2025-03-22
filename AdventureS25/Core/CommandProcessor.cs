using System;
using System.Text.RegularExpressions;

namespace AdventureS25.Core
{
    /// <summary>
    /// Processes player input into commands
    /// </summary>
    public static class CommandProcessor
    {
        /// <summary>
        /// Process a string input into a Command object
        /// </summary>
        public static Command Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Command("", "", input);
            }
            
            // Convert to lowercase and trim
            input = input.Trim();
            
            // Check for standalone verbs
            if (CommandValidator.IsStandaloneVerb(input))
            {
                return new Command(input, "", input);
            }
            
            // Split into verb and noun
            string[] parts = input.Split(new[] { ' ' }, 2);
            string verb = parts[0].ToLower();
            string noun = parts.Length > 1 ? parts[1].ToLower() : "";
            
            // Validate the command
            if (!CommandValidator.IsValidVerb(verb))
            {
                return new Command("invalid", input, input);
            }
            
            return new Command(verb, noun, input);
        }
    }
}
