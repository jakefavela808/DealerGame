using System;

namespace AdventureS25.Core
{
    /// <summary>
    /// Base class for quest objectives
    /// </summary>
    public abstract class QuestObjective
    {
        public string Description { get; private set; }
        public bool IsCompleted { get; protected set; }

        public QuestObjective(string description)
        {
            Description = description;
            IsCompleted = false;
        }

        /// <summary>
        /// Mark the objective as completed
        /// </summary>
        public void Complete()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                TextPrinter.Print($"Objective completed: {Description}");
            }
        }

        /// <summary>
        /// Check if the objective has been completed
        /// </summary>
        public abstract void CheckProgress();
    }

    /// <summary>
    /// Objective to deliver an item to a character
    /// </summary>
    public class ItemDeliveryObjective : QuestObjective
    {
        private readonly string itemName;
        private readonly Character targetCharacter;
        private bool hasOfferedToDeliver = false;
        
        public ItemDeliveryObjective(string description, string itemName, Character targetCharacter) 
            : base(description)
        {
            this.itemName = itemName;
            this.targetCharacter = targetCharacter;
        }
        
        public override void CheckProgress()
        {
            // This will be checked when talking to the target character
            // and having the required item in inventory
        }
        
        /// <summary>
        /// Check if the player can deliver the item to the target character
        /// </summary>
        public bool CheckDelivery(object? unused, Character character)
        {
            if (character != targetCharacter || IsCompleted)
            {
                return false;
            }
            
            Item? item = Player.Inventory.GetItem(itemName);
            
            if (item != null)
            {
                if (!hasOfferedToDeliver)
                {
                    TextPrinter.Print($"{targetCharacter.Name} says: \"I see you have {item.Name}. Type 'complete' to give it to me.\"");
                    hasOfferedToDeliver = true;
                    return false;
                }
                else
                {
                    Player.Inventory.RemoveItem(item);
                    TextPrinter.Print($"You give the {item.Name} to {targetCharacter.Name}.");
                    Complete();
                    return true;
                }
            }
            else
            {
                if (!hasOfferedToDeliver)
                {
                    TextPrinter.Print($"{targetCharacter.Name} says: \"Do you have the {itemName}? I've been waiting for it.\"");
                    hasOfferedToDeliver = true;
                }
                else
                {
                    TextPrinter.Print($"{targetCharacter.Name} says: \"Please find the {itemName} and bring it to me.\"");
                }
                return false;
            }
        }
    }

    /// <summary>
    /// Objective to talk to a character
    /// </summary>
    public class TalkToCharacterObjective : QuestObjective
    {
        private readonly Character targetCharacter;
        
        public TalkToCharacterObjective(string description, Character targetCharacter) 
            : base(description)
        {
            this.targetCharacter = targetCharacter;
        }
        
        public override void CheckProgress()
        {
            // This will be checked when talking to the target character
        }
        
        /// <summary>
        /// Check if the player is talking to the target character
        /// </summary>
        public bool CheckConversation(Character character)
        {
            if (character == targetCharacter && !IsCompleted)
            {
                Complete();
                return true;
            }
            
            return false;
        }
    }

    /// <summary>
    /// Objective to collect a certain number of items
    /// </summary>
    public class CollectionObjective : QuestObjective
    {
        private readonly string itemName;
        private readonly int requiredAmount;
        
        public CollectionObjective(string description, string itemName, int requiredAmount) 
            : base(description)
        {
            this.itemName = itemName;
            this.requiredAmount = requiredAmount;
        }
        
        public override void CheckProgress()
        {
            // Count how many of the required item the player has
            int count = 0;
            foreach (var item in Player.Inventory.GetItems())
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    count++;
                }
            }
            
            if (count >= requiredAmount && !IsCompleted)
            {
                Complete();
            }
        }
    }
}
