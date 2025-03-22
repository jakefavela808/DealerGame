using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Represents a quest status
    /// </summary>
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    /// <summary>
    /// Represents a quest that can be assigned to the player
    /// </summary>
    public class Quest
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Character QuestGiver { get; private set; }
        public QuestStatus Status { get; private set; }
        public List<QuestObjective> Objectives { get; private set; }
        public List<QuestReward> Rewards { get; private set; }
        
        // For tracking if the player has been offered this quest
        public bool HasBeenOffered { get; set; }

        public Quest(string id, string title, string description, Character questGiver)
        {
            Id = id;
            Title = title;
            Description = description;
            QuestGiver = questGiver;
            Status = QuestStatus.NotStarted;
            Objectives = new List<QuestObjective>();
            Rewards = new List<QuestReward>();
            HasBeenOffered = false;
        }

        /// <summary>
        /// Add an objective to this quest
        /// </summary>
        public void AddObjective(QuestObjective objective)
        {
            Objectives.Add(objective);
        }

        /// <summary>
        /// Add a reward to this quest
        /// </summary>
        public void AddReward(QuestReward reward)
        {
            Rewards.Add(reward);
        }

        /// <summary>
        /// Start the quest
        /// </summary>
        public void Start()
        {
            Status = QuestStatus.InProgress;
            QuestManager.AddActiveQuest(this);
        }

        /// <summary>
        /// Complete the quest and give rewards
        /// </summary>
        public void Complete()
        {
            Status = QuestStatus.Completed;
            
            TextPrinter.Print($"Quest completed: {Title}");
            
            // Give rewards
            foreach (var reward in Rewards)
            {
                reward.GiveReward();
            }
            
            QuestManager.RemoveActiveQuest(this);
        }

        /// <summary>
        /// Fail the quest
        /// </summary>
        public void Fail()
        {
            Status = QuestStatus.Failed;
            TextPrinter.Print($"Quest failed: {Title}");
            QuestManager.RemoveActiveQuest(this);
        }

        /// <summary>
        /// Check if all objectives are completed
        /// </summary>
        public void CheckProgress()
        {
            if (Status != QuestStatus.InProgress)
            {
                return;
            }
            
            bool allCompleted = Objectives.All(o => o.IsCompleted);
            
            if (allCompleted)
            {
                Complete();
            }
        }

        /// <summary>
        /// Get a string representation of the quest
        /// </summary>
        public string GetQuestString()
        {
            string result = $"{Title} - {Description}\nStatus: {Status}\n";
            
            result += "Objectives:";
            foreach (var objective in Objectives)
            {
                string status = objective.IsCompleted ? "[Completed]" : "[Incomplete]";
                result += $"\n- {objective.Description} {status}";
            }
            
            return result;
        }
    }
}
