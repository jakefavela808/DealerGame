using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureS25.Core
{
    /// <summary>
    /// Handles commands entered by the player
    /// </summary>
    public static class CommandHandler
    {
        private static Dictionary<string, Action<Command>> commandMap = new Dictionary<string, Action<Command>>()
        {
            {"go", Move},
            {"look", Look},
            {"take", Take},
            {"drop", Drop},
            {"inventory", ShowInventory},
            {"talk", Talk},
            {"help", Help},
            {"quit", Quit},
            {"quests", ShowQuests},
            {"accept", AcceptQuest},
            {"decline", DeclineQuest},
            {"complete", CompleteQuest}
        };
        
        /// <summary>
        /// Handle a command
        /// </summary>
        public static void Handle(Command command)
        {
            if (commandMap.ContainsKey(command.Verb))
            {
                commandMap[command.Verb].Invoke(command);
            }
            else if (command.Verb == "invalid")
            {
                TextPrinter.Print("I don't understand that command.");
            }
            else
            {
                TextPrinter.Print($"I don't know how to '{command.Verb}'.");
            }
        }

        private static void Move(Command command)
        {
            string? direction = command.Noun;
            
            // Handle shorthand directions
            if (direction == "n") direction = "north";
            if (direction == "s") direction = "south";
            if (direction == "e") direction = "east";
            if (direction == "w") direction = "west";
            
            if (string.IsNullOrEmpty(direction))
            {
                TextPrinter.Print("Go where?");
                return;
            }
            
            Player.Move(direction);
        }

        private static void Look(Command command)
        {
            Player.Look();
        }

        private static void Take(Command command)
        {
            if (string.IsNullOrEmpty(command.Noun))
            {
                TextPrinter.Print("Take what?");
                return;
            }
            
            Player.Take(command.Noun);
        }

        private static void Drop(Command command)
        {
            if (string.IsNullOrEmpty(command.Noun))
            {
                TextPrinter.Print("Drop what?");
                return;
            }
            
            Player.Drop(command.Noun);
        }

        private static void ShowInventory(Command command)
        {
            Player.ShowInventory();
        }

        private static void Talk(Command command)
        {
            if (string.IsNullOrEmpty(command.Noun))
            {
                TextPrinter.Print("Talk to whom?");
                return;
            }
            
            Player.TalkTo(command.Noun);
        }

        private static void Help(Command command)
        {
            TextPrinter.Print("Available commands:");
            TextPrinter.Print("- go [direction]: Move in a direction (north, south, east, west)");
            TextPrinter.Print("- look: Look around your current location");
            TextPrinter.Print("- take [item]: Take an item from your current location");
            TextPrinter.Print("- drop [item]: Drop an item from your inventory");
            TextPrinter.Print("- inventory: Show your inventory");
            TextPrinter.Print("- talk [character]: Talk to a character");
            TextPrinter.Print("- quests: Show your active quests");
            TextPrinter.Print("- accept: Accept a quest");
            TextPrinter.Print("- decline: Decline a quest");
            TextPrinter.Print("- complete: Complete a quest objective");
            TextPrinter.Print("- quit: Exit the game");
        }

        private static void Quit(Command command)
        {
            TextPrinter.Print("Are you sure you want to quit? (yes/no)");
            string? input = Console.ReadLine()?.ToLower();
            
            if (input == "yes" || input == "y")
            {
                Game.End();
            }
        }

        private static void ShowQuests(Command command)
        {
            QuestManager.ShowQuestLog();
        }

        private static void AcceptQuest(Command command)
        {
            // Find a character with available quests
            Character? questGiver = null;
            if (Player.CurrentLocation != null)
            {
                foreach (var character in World.GetCharactersAtLocation(Player.CurrentLocation))
                {
                    if (character.GetAvailableQuests().Count > 0 && 
                        character.GetAvailableQuests().Any(q => q.HasBeenOffered))
                    {
                        questGiver = character;
                        break;
                    }
                }
            }
            
            if (questGiver == null)
            {
                TextPrinter.Print("There are no quests to accept here.");
                return;
            }
            
            // If no specific quest number is provided, and there's only one quest, accept it
            if (string.IsNullOrEmpty(command.Noun))
            {
                if (questGiver.GetAvailableQuests().Count == 1)
                {
                    questGiver.AcceptQuest(0);
                }
                else if (questGiver.GetAvailableQuests().Count > 1)
                {
                    TextPrinter.Print("Please specify which quest to accept (e.g., 'accept 1').");
                    questGiver.OfferQuests();
                }
                else
                {
                    TextPrinter.Print("There are no quests to accept here.");
                }
                return;
            }
            
            // Try to parse the quest number
            if (int.TryParse(command.Noun, out int questIndex))
            {
                // Quest indices are displayed 1-based, but stored 0-based
                questGiver.AcceptQuest(questIndex - 1);
            }
            else
            {
                TextPrinter.Print("Please specify a valid quest number to accept.");
            }
        }

        private static void DeclineQuest(Command command)
        {
            // Find a character with available quests
            Character? questGiver = null;
            if (Player.CurrentLocation != null)
            {
                foreach (var character in World.GetCharactersAtLocation(Player.CurrentLocation))
                {
                    if (character.GetAvailableQuests().Count > 0 && 
                        character.GetAvailableQuests().Any(q => q.HasBeenOffered))
                    {
                        questGiver = character;
                        break;
                    }
                }
            }
            
            if (questGiver == null)
            {
                TextPrinter.Print("There are no quests to decline here.");
                return;
            }
            
            TextPrinter.Print($"You declined {questGiver.Name}'s quest.");
            
            // Reset the HasBeenOffered flag for all quests
            foreach (var quest in questGiver.GetAvailableQuests())
            {
                quest.HasBeenOffered = false;
            }
        }

        private static void CompleteQuest(Command command)
        {
            Player.CompleteQuest(command.Noun);
        }
    }
}
