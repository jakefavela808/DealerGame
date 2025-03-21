namespace AdventureS25;

public class Character
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Location CurrentLocation { get; set; }
    public List<string> Dialogue { get; private set; }
    public bool CanMove { get; set; }

    public Character(string name, string description, Location location, bool canMove = false)
    {
        Name = name;
        Description = description;
        CurrentLocation = location;
        Dialogue = new List<string>();
        CanMove = canMove;
    }

    public void AddDialogue(string dialogueLine)
    {
        Dialogue.Add(dialogueLine);
    }

    public string GetRandomDialogue()
    {
        if (Dialogue.Count == 0)
        {
            return $"{Name} has nothing to say.";
        }

        Random random = new Random();
        int index = random.Next(Dialogue.Count);
        return Dialogue[index];
    }

    public void MoveTo(Location newLocation)
    {
        if (CanMove)
        {
            CurrentLocation = newLocation;
        }
    }

    public override string ToString()
    {
        return Name;
    }
}
