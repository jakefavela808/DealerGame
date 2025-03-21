namespace AdventureS25;

public static class Player
{
    public static Location CurrentLocation;
    public static List<Item> Inventory;

    public static void Initialize()
    {
        Inventory = new List<Item>();
        CurrentLocation = Map.StartLocation;
    }

    public static void Move(Command command)
    {
        PlayerMovement.Move(command);
    }

    public static string GetLocationDescription()
    {
        return PlayerMovement.GetLocationDescription();
    }

    public static void Take(Command command)
    {
        PlayerInventory.Take(command);
    }

    public static void ShowInventory()
    {
        PlayerInventory.ShowInventory();
    }

    public static void Look()
    {
        PlayerMovement.Look();
        PlayerCharacterInteraction.ListCharactersAtLocation();
    }

    public static void Drop(Command command)
    {       
        PlayerInventory.Drop(command);
    }

    public static void TellStory(Command command)
    {
        PlayerCharacterInteraction.TellStory(command);
    }

    public static void AddCharacter(string name, string description, Location location, bool canMove = false)
    {
        PlayerCharacterInteraction.AddCharacter(name, description, location, canMove);
    }

    public static void MoveCharacter(string characterName, Location newLocation)
    {
        PlayerCharacterInteraction.MoveCharacter(characterName, newLocation);
    }

    public static void AddDialogueToCharacter(string characterName, string dialogue)
    {
        PlayerCharacterInteraction.AddDialogueToCharacter(characterName, dialogue);
    }
}