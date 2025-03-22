using System;

namespace AdventureS25.Core
{
    /// <summary>
    /// Base class for quest rewards
    /// </summary>
    public abstract class QuestReward
    {
        public string Description { get; private set; }

        public QuestReward(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Give the reward to the player
        /// </summary>
        public abstract void GiveReward();
    }

    /// <summary>
    /// Reward that gives money to the player
    /// </summary>
    public class MoneyReward : QuestReward
    {
        private readonly int amount;

        public MoneyReward(string description, int amount) : base(description)
        {
            this.amount = amount;
        }

        public override void GiveReward()
        {
            Player.Money += amount;
            TextPrinter.Print($"Received {amount} gold.");
        }
    }

    /// <summary>
    /// Reward that gives an item to the player
    /// </summary>
    public class ItemReward : QuestReward
    {
        private readonly Item item;

        public ItemReward(string description, Item item) : base(description)
        {
            this.item = item;
        }

        public override void GiveReward()
        {
            Player.Inventory.AddItem(item);
            TextPrinter.Print($"Received item: {item.Name}");
        }
    }
}
