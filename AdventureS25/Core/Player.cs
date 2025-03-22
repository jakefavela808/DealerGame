using System;
using System.Collections.Generic;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents the player character
    /// </summary>
    public static class Player
    {
        public static Location? CurrentLocation { get; set; }
        public static Inventory Inventory { get; set; } = new Inventory();
        public static int Money { get; set; }

        static Player()
        {
            Money = 0;
        }

        /// <summary>
        /// Move the player in a specific direction
        /// </summary>
        public static void Move(string direction)
        {
            Location? newLocation = CurrentLocation?.GetExit(direction);
            
            if (newLocation == null)
            {
                TextPrinter.Print($"You can't go {direction}.");
                return;
            }
            
            CurrentLocation = newLocation;
            Look();
        }
        
        /// <summary>
        /// Look around the current location
        /// </summary>
        public static void Look()
        {
            if (CurrentLocation == null)
            {
                TextPrinter.Print("You are nowhere.");
                return;
            }
            
            TextPrinter.Print($"=== {CurrentLocation.Name} ===");
            TextPrinter.Print(CurrentLocation.Description);
            
            // Show exits
            var exits = CurrentLocation.GetExits();
            if (exits.Count > 0)
            {
                TextPrinter.Print("\nExits:");
                foreach (var exit in exits)
                {
                    TextPrinter.Print($"- {exit.Key}: {exit.Value.Name}");
                }
            }
            
            // Show items
            var items = CurrentLocation.GetItems();
            if (items.Count > 0)
            {
                TextPrinter.Print("\nItems:");
                foreach (var item in items)
                {
                    TextPrinter.Print($"- {item.Name}");
                }
            }
            
            // Show characters
            var characters = World.GetCharactersAtLocation(CurrentLocation);
            if (characters.Count > 0)
            {
                TextPrinter.Print("\nCharacters:");
                foreach (var character in characters)
                {
                    TextPrinter.Print($"- {character.Name}");
                }
            }
        }
        
        /// <summary>
        /// Take an item from the current location
        /// </summary>
        public static void Take(string itemName)
        {
            if (CurrentLocation != null)
            {
                Item? item = CurrentLocation.GetItem(itemName);
                
                if (item == null)
                {
                    TextPrinter.Print($"There is no {itemName} here.");
                    return;
                }
                
                Inventory.AddItem(item);
                CurrentLocation.RemoveItem(item);
                TextPrinter.Print($"You took the {item.Name}.");
            }
        }
        
        /// <summary>
        /// Drop an item from the inventory
        /// </summary>
        public static void Drop(string itemName)
        {
            if (CurrentLocation != null)
            {
                Item? item = Inventory.GetItem(itemName);
                
                if (item == null)
                {
                    TextPrinter.Print($"You don't have a {itemName}.");
                    return;
                }
                
                Inventory.RemoveItem(item);
                CurrentLocation.AddItem(item);
                TextPrinter.Print($"You dropped the {item.Name}.");
            }
        }
        
        /// <summary>
        /// Show the player's inventory
        /// </summary>
        public static void ShowInventory()
        {
            TextPrinter.Print(Inventory.GetInventoryString());
            TextPrinter.Print($"Money: {Money} gold");
        }
        
        /// <summary>
        /// Talk to a character at the current location
        /// </summary>
        public static void TalkTo(string characterName)
        {
            if (CurrentLocation != null)
            {
                List<Character> characters = World.GetCharactersAtLocation(CurrentLocation);
                Character? character = characters.Find(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
                
                if (character == null)
                {
                    TextPrinter.Print($"There is no {characterName} here.");
                    return;
                }
                
                string dialogue = character.GetNextDialogue();
                TextPrinter.Print(dialogue);
                
                // Check if the character has quests to offer
                if (character.GetAvailableQuests().Count > 0)
                {
                    character.OfferQuests();
                }
                
                // Try to complete quests with this character
                TryCompleteQuestWithCharacter(character);
            }
        }
        
        /// <summary>
        /// Try to complete a quest with a character
        /// </summary>
        private static bool TryCompleteQuestWithCharacter(Character character)
        {
            bool completedAny = false;
            
            foreach (var quest in QuestManager.GetActiveQuests())
            {
                if (quest.Status == QuestStatus.Completed)
                {
                    continue; // Skip already completed quests
                }
                
                // Check each objective in the quest
                foreach (var objective in quest.Objectives)
                {
                    if (objective is ItemDeliveryObjective deliveryObjective)
                    {
                        if (deliveryObjective.CheckDelivery(null, character))
                        {
                            completedAny = true;
                        }
                    }
                }
                
                // Check if all objectives are completed
                quest.CheckProgress();
                
                // If the quest is now completed, it will have been handled in CheckProgress
            }
            
            return completedAny;
        }
        
        /// <summary>
        /// Complete a quest with a specific character
        /// </summary>
        public static void CompleteQuest(string? characterName)
        {
            if (CurrentLocation == null)
            {
                return;
            }
            
            if (string.IsNullOrEmpty(characterName))
            {
                // Try to complete with any character at the location
                foreach (var character in World.GetCharactersAtLocation(CurrentLocation))
                {
                    if (TryCompleteQuestWithCharacter(character))
                    {
                        return;
                    }
                }
                
                TextPrinter.Print("You don't have any quests to complete here.");
            }
            else
            {
                // Try to complete with a specific character
                Character? character = World.GetCharactersAtLocation(CurrentLocation)
                    .Find(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
                
                if (character == null)
                {
                    TextPrinter.Print($"There is no {characterName} here.");
                    return;
                }
                
                if (!TryCompleteQuestWithCharacter(character))
                {
                    TextPrinter.Print($"You don't have any quests to complete with {character.Name}.");
                }
            }
        }
    }
}
