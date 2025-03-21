namespace AdventureS25;

public static class PlayerCharacterInteraction
{
    public static void AddCharacter(string name, string description, Location location, bool canMove = false)
    {
        if (Characters.CharacterExists(name))
        {
            Console.WriteLine($"A character named {name} already exists.");
            return;
        }

        Character character = new Character(name, description, location, canMove);
        Characters.AddCharacter(character);
        Console.WriteLine($"Added character {name} to {location}.");
    }

    public static void TellStory(Command command)
    {
        string characterName = command.Noun;
        Character? character = Characters.GetCharacterByName(characterName);

        if (character == null)
        {
            Console.WriteLine($"There is no one named {characterName} here.");
            return;
        }

        if (character.CurrentLocation != Player.CurrentLocation)
        {
            Console.WriteLine($"{characterName} is not here.");
            return;
        }

        string dialogue = character.GetRandomDialogue();
        Console.WriteLine($"{character.Name}: \"{dialogue}\"");
    }

    public static void ListCharactersAtLocation()
    {
        List<Character> charactersHere = Characters.GetCharactersAtLocation(Player.CurrentLocation);
        
        if (charactersHere.Count == 0)
        {
            Console.WriteLine("There is no one else here.");
            return;
        }

        Console.WriteLine("You see:");
        foreach (Character character in charactersHere)
        {
            Console.WriteLine($" {character.Name} - {character.Description}");
        }
    }

    public static void MoveCharacter(string characterName, Location newLocation)
    {
        Character? character = Characters.GetCharacterByName(characterName);
        
        if (character == null)
        {
            Console.WriteLine($"There is no one named {characterName}.");
            return;
        }

        if (!character.CanMove)
        {
            Console.WriteLine($"{characterName} cannot move from their location.");
            return;
        }

        character.MoveTo(newLocation);
        Console.WriteLine($"{characterName} has moved to {newLocation}.");
    }

    public static void AddDialogueToCharacter(string characterName, string dialogue)
    {
        Character? character = Characters.GetCharacterByName(characterName);
        
        if (character == null)
        {
            Console.WriteLine($"There is no one named {characterName}.");
            return;
        }

        character.AddDialogue(dialogue);
        Console.WriteLine($"Added dialogue to {characterName}.");
    }
}
