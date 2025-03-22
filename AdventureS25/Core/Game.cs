using System;
using System.Collections.Generic;

namespace AdventureS25.Core
{
    /// <summary>
    /// Main game class that initializes and runs the game
    /// </summary>
    public static class Game
    {
        public static bool IsRunning { get; private set; }

        static Game()
        {
            IsRunning = false;
        }

        /// <summary>
        /// Initialize the game world, characters, items, and quests
        /// </summary>
        public static void Initialize()
        {
            // Create the game world
            CreateWorld();
            
            // Create items
            CreateItems();
            
            // Create characters
            CreateCharacters();
            
            // Create quests
            CreateQuests();
            
            // Set player's starting location
            Player.CurrentLocation = World.GetLocation("starting_location_id");
            
            // Initialize player inventory
            Player.Inventory = new Inventory();
            Player.Money = 100; // Starting money
        }

        /// <summary>
        /// Create the game world with locations and connections
        /// </summary>
        private static void CreateWorld()
        {
            // COPY-PASTE TEMPLATE:
            // Create locations
            /*
            Location town = new Location(
                "town",                  // ID
                "Town Square",           // Name
                "A bustling town square with shops and people."  // Description
            );
            
            Location forest = new Location(
                "forest",
                "Dark Forest",
                "A dense, dark forest with towering trees."
            );
            
            // Add locations to the world
            World.AddLocation(town);
            World.AddLocation(forest);
            
            // Connect locations
            town.AddExit("north", forest);
            forest.AddExit("south", town);
            */
        }

        /// <summary>
        /// Create items that can be found in the game
        /// </summary>
        private static void CreateItems()
        {
            // COPY-PASTE TEMPLATE:
            // Create items
            /*
            Item sword = new Item(
                "sword",                // ID
                "Rusty Sword",          // Name
                "An old rusty sword.",  // Description
                5                       // Value
            );
            
            Item potion = new Item(
                "potion",
                "Health Potion",
                "A red potion that restores health.",
                10
            );
            
            // Add items to locations
            World.GetLocation("town").AddItem(sword);
            
            // Or add items to characters
            World.GetCharacter("merchant").AddItem(potion);
            */
        }

        /// <summary>
        /// Create characters in the game world
        /// </summary>
        private static void CreateCharacters()
        {
            // COPY-PASTE TEMPLATE:
            // Create characters
            /*
            Character merchant = new Character(
                "merchant",              // ID
                "Town Merchant",         // Name
                "A friendly merchant selling various goods.",  // Description
                World.GetLocation("town")  // Starting location
            );
            
            // Add dialogue
            merchant.AddDialogue("Welcome to my shop! What would you like to buy?");
            merchant.AddDialogue("I have the finest goods in town!");
            
            // Add character to the world
            World.AddCharacter(merchant);
            */
        }

        /// <summary>
        /// Create quests that can be assigned to the player
        /// </summary>
        private static void CreateQuests()
        {
            // COPY-PASTE TEMPLATE:
            // Create quests
            /*
            Quest deliveryQuest = new Quest(
                "delivery_quest",        // ID
                "Package Delivery",      // Title
                "Deliver a package to the forest hermit.",  // Description
                World.GetCharacter("merchant")  // Quest giver
            );
            
            // Create objectives
            QuestObjective talkObjective = new TalkToCharacterObjective(
                "Talk to the hermit in the forest",  // Description
                World.GetCharacter("hermit")         // Target character
            );
            
            QuestObjective deliverObjective = new ItemDeliveryObjective(
                "Deliver the package to the hermit",  // Description
                "package",                           // Item name
                World.GetCharacter("hermit")         // Target character
            );
            
            // Add objectives to quest
            deliveryQuest.AddObjective(talkObjective);
            deliveryQuest.AddObjective(deliverObjective);
            
            // Create rewards
            QuestReward goldReward = new MoneyReward("Gold for your trouble", 50);
            QuestReward itemReward = new ItemReward("A token of appreciation", new Item("amulet", "Magic Amulet", "A glowing amulet.", 100));
            
            // Add rewards to quest
            deliveryQuest.AddReward(goldReward);
            deliveryQuest.AddReward(itemReward);
            
            // Add quest to character
            World.GetCharacter("merchant").AddQuest(deliveryQuest);
            */
        }

        /// <summary>
        /// Start the game loop
        /// </summary>
        public static void Start()
        {
            IsRunning = true;
            
            // Display welcome message
            TextPrinter.Print("Welcome to the Adventure Game!");
            TextPrinter.Print("Type 'help' for a list of commands.");
            
            // Show initial location description
            Player.Look();
            
            // Main game loop
            while (IsRunning)
            {
                // Get player input
                Console.Write("> ");
                string input = Console.ReadLine() ?? string.Empty;
                
                // Process command
                Command command = CommandProcessor.Process(input);
                
                // Handle command
                CommandHandler.Handle(command);
            }
        }

        /// <summary>
        /// End the game
        /// </summary>
        public static void End()
        {
            IsRunning = false;
            TextPrinter.Print("Thanks for playing! Goodbye.");
        }
    }
}
