namespace AdventureS25;

public static class Items
{
    private static Dictionary<string, Item> nameToItem = 
        new Dictionary<string, Item>();
    
    public static void Initialize()
    {
        // Create and place items in one step
        CreateAndPlaceItem("sword", "long sword", "There is a long sword stuck in a rock here.", "Entrance");
        CreateAndPlaceItem("donut", "A giant concrete donut that you can't take", "A giant concrete donut rests on the floor.", "Storage", false);
        CreateAndPlaceItem("beer", "beer's beer", "There is a beer's beer in a beer here.", "Throne Room");
        CreateAndPlaceItem("apple", "a shiny rotten apple", "A shiny rotten apple sits on the floor.", "Entrance");
        CreateAndPlaceItem("spear", "a shiny rotten spear", "A shiny rotten spear sits is propped in the corner.", "Entrance");
    }

    // Helper method to create an item and place it in a location
    private static void CreateAndPlaceItem(string name, string description, string locationDescription, string locationName, bool isTakeable = true)
    {
        Item item = new Item(name, description, locationDescription, isTakeable);
        nameToItem.Add(name, item);
        Map.AddItem(name, locationName);
    }

    // Public method to add new items to the game
    public static void AddItem(string name, string description, string locationDescription, string locationName, bool isTakeable = true)
    {
        if (!nameToItem.ContainsKey(name))
        {
            CreateAndPlaceItem(name, description, locationDescription, locationName, isTakeable);
            CommandValidator.AddNoun(name);
        }
    }

    public static Item? GetItemByName(string itemName)
    {
        if (nameToItem.ContainsKey(itemName))
        {
            return nameToItem[itemName];
        }
        return null;
    }
}