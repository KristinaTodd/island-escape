using System.Collections.Generic;
using island_escape.Interfaces;
using island_escape.Models;

namespace island_escape.Services
{
  class GameService : IGameService
  {
    public List<string> Messages { get; set; }
    private IGame _game { get; set; }

    public GameService(string playerName)
    {
      Messages = new List<string>();
      _game = new Game();
      _game.CurrentPlayer = new Player(playerName);
      Look();
    }

    public bool Go(string direction)
    {
      //if the current room has that direction on the exits dictionary
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        // set current room to the exit room
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        // populate messages with room description
        Messages.Add($"You travel {direction}, and discover: ");
        Look();
        EndRoom end = _game.CurrentRoom as EndRoom;
        if (end != null)
        {
          Messages.Add(end.Narrative);
          return false;
        }
        return true;
      }
      //no exit in that direction
      Messages.Add("No path in that direction");
      Look();
      return true;
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      Messages.Add("Current Inventory: ");
      foreach (var item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{item.Name} - {item.Description}");
      }
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Name);
      Messages.Add(_game.CurrentRoom.Description);
      if (_game.CurrentRoom.Items.Count > 0)
      {
        Messages.Add("There are a few things within reach:");
        foreach (var item in _game.CurrentRoom.Items)
        {
          Messages.Add("     " + item.Name);
        }
      }
      string exits = string.Join(", ", _game.CurrentRoom.Exits.Keys);
      Messages.Add("There's a path to the " + exits);

      string lockedExits = "";
      foreach (var lockedRoom in _game.CurrentRoom.LockedExits.Values)
      {
        lockedExits += lockedRoom.Key;
      }
      Messages.Add("There's a path to " + lockedExits + "but you must move a fallen tree.");

    }

    public void Reset()
    {
      string name = _game.CurrentPlayer.Name;
      _game = new Game();
      _game.CurrentPlayer = new Player(name);
    }

    public void Take(string itemName)
    {
      IItem found = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        _game.CurrentPlayer.Inventory.Add(found);
        _game.CurrentRoom.Items.Remove(found);
        Messages.Add($"You have taken the {itemName}");
        return;
      }
      Messages.Add("Cannot find item by that name");
    }

    public void Use(string itemName)
    {
      var found = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (found != null)
      {
        Messages.Add(_game.CurrentRoom.Use(found));
        return;
      }
      // check if item is in room
      Messages.Add("You don't have that Item");
    }
  }
}