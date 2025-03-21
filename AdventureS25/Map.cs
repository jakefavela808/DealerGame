namespace AdventureS25;

public static class Map
{
    private static Dictionary<string, Location> nameToLocation = 
        new Dictionary<string, Location>();
    public static Location StartLocation;
    
    public static void Initialize()
    {
        Location entrance = new Location("Entrance", 
            "This is the entrance room. Everything starts here.");
        nameToLocation.Add("Entrance", entrance);
        
        Location storage = new Location("Storage", 
            "You are in a small storage room. There are lots of things.");
        nameToLocation.Add("Storage", storage);
        
        Location throne = new Location("Throne Room", 
            "There is a big ass throne here.");
        nameToLocation.Add("Throne Room", throne);
        
        entrance.AddConnection("east", storage);
        storage.AddConnection("west", entrance);
        throne.AddConnection("south", entrance);
        entrance.AddConnection("north", throne);

        StartLocation = entrance;
    }
    

    public static void AddItem(string itemName, string locationName)
    {
        // find out which Location is named locationName
        Location location = GetLocationByName(locationName);
        Item item = Items.GetItemByName(itemName);
        
        // add the item to the location
        if (item != null && location != null)
        {
            location.AddItem(item);
        }
    }

    private static Location GetLocationByName(string locationName)
    {
        if (nameToLocation.ContainsKey(locationName))
        {
            return nameToLocation[locationName];
        }
        else
        {
            return null;
        }
    }
}