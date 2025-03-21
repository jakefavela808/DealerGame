namespace AdventureS25;

public static class Characters
{
    private static List<Character> _characters = new List<Character>();

    public static void Initialize()
    {
        _characters.Clear();
        // You can add default characters here if needed
    }

    public static void AddCharacter(Character character)
    {
        _characters.Add(character);
    }

    public static Character? GetCharacterByName(string name)
    {
        return _characters.Find(c => c.Name.ToLower() == name.ToLower());
    }

    public static List<Character> GetCharactersAtLocation(Location location)
    {
        return _characters.FindAll(c => c.CurrentLocation == location);
    }

    public static void RemoveCharacter(Character character)
    {
        _characters.Remove(character);
    }

    public static bool CharacterExists(string name)
    {
        return _characters.Exists(c => c.Name.ToLower() == name.ToLower());
    }
}
