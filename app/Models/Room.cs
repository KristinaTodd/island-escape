using System.Collections.Generic;
using island_escape.Interfaces;
using island_escape.Services;

namespace island_escape.Models
{
  class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IItem> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Dictionary<IItem, KeyValuePair<string, IRoom>> LockedExits { get; set; }

    public IPlayer CurrentPlayer { get; set; }
    public IRoom CurrentRoom { get; set; }
    public void AddLockedRoom(IItem key, string direction, IRoom room)
    {
      var lockedRoom = new KeyValuePair<string, IRoom>(direction, room);
      LockedExits.Add(key, lockedRoom);
    }

    public string Use(IItem item)
    {
      if (LockedExits.ContainsKey(item))
      {
        Exits.Add(LockedExits[item].Key, LockedExits[item].Value);
        LockedExits.Remove(item);
        if (item.Name.ToLower() == "machete")
        {
          return "You use the machete and slash anything in your way! You can now see the path to the east.";
        }
        if (item.Name.ToLower() == "torch and matches")
        {
          return "You can now see around the cave! There's a wooden paddle over there in the corner! You also see an exit to the north.";
        }
        if (item.Name.ToLower() == "row boat")
        {
          return "You are now in the row boat. This rowboat is pretty worn...Will it make it to the ship?";
        }
        if (item.Name.ToLower() == "wooden paddle")
        {
          return "You are now able to row south towards the ship!";
        }
      }
      return "No use for that here";
    }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<IItem>();
      Exits = new Dictionary<string, IRoom>();
      LockedExits = new Dictionary<IItem, KeyValuePair<string, IRoom>>();
    }
  }
}