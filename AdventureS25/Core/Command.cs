using System;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents a command entered by the player
    /// </summary>
    public class Command
    {
        public string Verb { get; private set; }
        public string? Noun { get; private set; }
        public string FullText { get; private set; }

        public Command(string verb, string? noun, string fullText)
        {
            Verb = verb.ToLower();
            Noun = noun?.ToLower();
            FullText = fullText;
        }
    }
}
