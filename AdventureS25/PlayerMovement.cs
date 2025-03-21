namespace AdventureS25;

public static class PlayerMovement
{
    public static void Move(Command command)
    {
        if (Player.CurrentLocation.CanMoveInDirection(command))
        {
            Player.CurrentLocation = Player.CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(Player.CurrentLocation.GetDescription());
        }
        else
        {
            Console.WriteLine("You can't move " + command.Noun + ".");
        }
    }

    public static string GetLocationDescription()
    {
        return Player.CurrentLocation.GetDescription();
    }

    public static void Look()
    {
        Console.WriteLine(Player.CurrentLocation.GetDescription());
    }
}
