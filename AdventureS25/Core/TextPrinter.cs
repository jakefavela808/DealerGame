using System;
using System.Threading;

namespace AdventureS25.Core
{
    /// <summary>
    /// Utility class for printing text with optional typing effect
    /// </summary>
    public class TextPrinter
    {
        private static bool useTypingEffect = true;
        private static int typingSpeed = 20; // milliseconds per character
        
        /// <summary>
        /// Print text with optional typing effect
        /// </summary>
        public static void Print(string text)
        {
            if (useTypingEffect)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(typingSpeed);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(text);
            }
        }
        
        /// <summary>
        /// Enable typing effect
        /// </summary>
        public static void EnableTypingEffect()
        {
            useTypingEffect = true;
        }
        
        /// <summary>
        /// Disable typing effect
        /// </summary>
        public static void DisableTypingEffect()
        {
            useTypingEffect = false;
        }
        
        /// <summary>
        /// Set typing speed (milliseconds per character)
        /// </summary>
        public static void SetTypingSpeed(int speed)
        {
            typingSpeed = speed;
        }
    }
}
