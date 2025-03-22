using System;
using System.Collections.Generic;

namespace AdventureS25.Core
{
    /// <summary>
    /// Static class that manages active quests
    /// </summary>
    public static class QuestManager
    {
        private static List<Quest> activeQuests = new List<Quest>();

        /// <summary>
        /// Add a quest to the active quests list
        /// </summary>
        public static void AddActiveQuest(Quest quest)
        {
            if (!activeQuests.Contains(quest))
            {
                activeQuests.Add(quest);
            }
        }

        /// <summary>
        /// Remove a quest from the active quests list
        /// </summary>
        public static void RemoveActiveQuest(Quest quest)
        {
            activeQuests.Remove(quest);
        }

        /// <summary>
        /// Get all active quests
        /// </summary>
        public static List<Quest> GetActiveQuests()
        {
            return activeQuests;
        }

        /// <summary>
        /// Show the quest log to the player
        /// </summary>
        public static void ShowQuestLog()
        {
            if (activeQuests.Count == 0)
            {
                TextPrinter.Print("You don't have any active quests.");
                return;
            }
            
            TextPrinter.Print("Quest Log:");
            foreach (var quest in activeQuests)
            {
                TextPrinter.Print(quest.GetQuestString());
                TextPrinter.Print("-------------------");
            }
        }

        /// <summary>
        /// Reset all quests (for testing or restarting)
        /// </summary>
        public static void Reset()
        {
            activeQuests.Clear();
        }
    }
}
