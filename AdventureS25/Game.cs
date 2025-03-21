namespace AdventureS25;

public static class Game
{
    public static void PlayGame()
    {
        Initialize();

        Console.WriteLine(Player.GetLocationDescription());
        
        bool isPlaying = true;
        
        while (isPlaying == true)
        {
            Command command = CommandProcessor.Process();
            
            if (command.IsValid)
            {
                if (command.Verb == "exit")
                {
                    Console.WriteLine("Game Over!");
                    isPlaying = false;
                }
                else
                {
                    CommandHandler.Handle(command);
                }
            }
        }
    }

    private static void Initialize()
    {
        Map.Initialize();
        Items.Initialize();
        Characters.Initialize();
        Player.Initialize();
        
        // Add a sample character to demonstrate functionality
        Location startLocation = Map.StartLocation;
        Player.AddCharacter("Guard", "A stern-looking guard standing at attention.", startLocation, false);
        Player.AddDialogueToCharacter("Guard", "Move along, citizen. Nothing to see here.");
        Player.AddDialogueToCharacter("Guard", "I used to be an adventurer like you, then I took an arrow to the knee.");
    }
}