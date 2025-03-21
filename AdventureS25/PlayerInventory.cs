namespace AdventureS25;

public static class PlayerInventory
{
    public static void Take(Command command)
    {
        // figure out which item to take: turn the noun into an item
        Item? item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            Console.WriteLine("I don't know what " + command.Noun + " is.");
        }
        else if (!Player.CurrentLocation.HasItem(item))
        {
            Console.WriteLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            Console.WriteLine("The " + command.Noun + " can't be taked.");
        }
        else
        {
            Player.Inventory.Add(item);
            Player.CurrentLocation.RemoveItem(item);
            item.Pickup();
            Console.WriteLine("You take the " + command.Noun + ".");
        }
    }

    public static void ShowInventory()
    {
        if (Player.Inventory.Count == 0)
        {
            Console.WriteLine("You are empty-handed.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (Item item in Player.Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Console.WriteLine(" " + article + " " + item.Name);
            }
        }
    }

    public static void Drop(Command command)
    {       
        Item? item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            string article = SemanticTools.CreateArticle(command.Noun);
            Console.WriteLine("I don't know what " + article + " " + command.Noun + " is.");
        }
        else if (!Player.Inventory.Contains(item))
        {
            Console.WriteLine("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Player.Inventory.Remove(item);
            Player.CurrentLocation.AddItem(item);
            Console.WriteLine("You drop the " + command.Noun + ".");
        }
    }
}
