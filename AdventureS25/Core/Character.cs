using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents a character in the game world
    /// </summary>
    public class Character
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Location CurrentLocation { get; set; }
        
        private List<string> dialogue;
        private List<Quest> availableQuests;
        private Inventory inventory;
        private int dialogueIndex;

        public Character(string id, string name, string description, Location location)
        {
            Id = id;
            Name = name;
            Description = description;
            CurrentLocation = location;
            dialogue = new List<string>();
            availableQuests = new List<Quest>();
            inventory = new Inventory();
            dialogueIndex = 0;
        }

        /// <summary>
        /// Add a dialogue line for this character
        /// </summary>
        public void AddDialogue(string dialogueLine)
        {
            dialogue.Add(dialogueLine);
        }

        /// <summary>
        /// Get the next dialogue line from this character
        /// </summary>
        public string GetNextDialogue()
        {
            if (dialogue.Count == 0)
            {
                return $"{Name} has nothing to say.";
            }
            
            string response = dialogue[dialogueIndex];
            dialogueIndex = (dialogueIndex + 1) % dialogue.Count;
            return $"{Name} says: \"{response}\"";
        }

        /// <summary>
        /// Add a quest that this character can give to the player
        /// </summary>
        public void AddQuest(Quest quest)
        {
            availableQuests.Add(quest);
        }

        /// <summary>
        /// Get all quests available from this character
        /// </summary>
        public List<Quest> GetAvailableQuests()
        {
            return availableQuests.Where(q => q.Status == QuestStatus.NotStarted).ToList();
        }

        /// <summary>
        /// Offer all available quests to the player
        /// </summary>
        public void OfferQuests()
        {
            List<Quest> quests = GetAvailableQuests();
            
            if (quests.Count == 0)
            {
                TextPrinter.Print($"{Name} has no quests available.");
                return;
            }
            
            TextPrinter.Print($"{Name} has a quest available:");
            
            for (int i = 0; i < quests.Count; i++)
            {
                Quest quest = quests[i];
                quest.HasBeenOffered = true;
                TextPrinter.Print($"{i+1}. {quest.Title}: {quest.Description}");
            }
            
            TextPrinter.Print("Type 'accept' to accept the quest or 'decline' to decline.");
        }

        /// <summary>
        /// Accept a quest from this character
        /// </summary>
        public void AcceptQuest(int questIndex)
        {
            List<Quest> quests = GetAvailableQuests();
            
            if (questIndex < 0 || questIndex >= quests.Count)
            {
                TextPrinter.Print("Invalid quest number.");
                return;
            }
            
            Quest quest = quests[questIndex];
            
            // Check if player already has an active quest
            if (QuestManager.GetActiveQuests().Count > 0)
            {
                TextPrinter.Print("You already have an active quest. Complete it before accepting a new one.");
                return;
            }
            
            quest.Start();
            TextPrinter.Print($"You accepted the quest: {quest.Title}");
            TextPrinter.Print(quest.Description);
            
            foreach (var objective in quest.Objectives)
            {
                TextPrinter.Print($"- {objective.Description}");
            }
        }

        /// <summary>
        /// Add an item to this character's inventory
        /// </summary>
        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        /// <summary>
        /// Remove an item from this character's inventory
        /// </summary>
        public void RemoveItem(Item item)
        {
            inventory.RemoveItem(item);
        }

        /// <summary>
        /// Get an item from this character's inventory
        /// </summary>
        public Item? GetItem(string itemName)
        {
            return inventory.GetItem(itemName);
        }

        /// <summary>
        /// Check if this character has a specific item
        /// </summary>
        public bool HasItem(string itemName)
        {
            return inventory.HasItem(itemName);
        }
    }
}
