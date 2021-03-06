using System.Collections.Generic;
using island_escape.Interfaces;
using island_escape.Models;
using island_escape.Controllers;

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
      Messages.Add(@"
      Your options are to:
      
      look
      take
      go (north,south,east,west)
      inventory
      use

      ");
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
        if (_game.CurrentRoom.Name.Equals("Dark Cave"))
        {
          Messages.Add("You can't even see if there are any items in here.");
        }
        else
        {
          Messages.Add("There are a few things within reach:");
          foreach (var item in _game.CurrentRoom.Items)
          {
            Messages.Add("     " + item.Name);
          }
        }
      }
      string exits = string.Join(" - ", _game.CurrentRoom.Exits.Keys);

      if (_game.CurrentRoom.Name.Equals("Cliffside"))
      {
        Messages.Add("There's a path to the " + exits + ". \n");
      }
      string lockedExits = "";
      foreach (var lockedRoom in _game.CurrentRoom.LockedExits.Values)
      {
        lockedExits += lockedRoom.Key;
      }
      if (_game.CurrentRoom.Name.Equals("Cliffside"))
      {
        Messages.Add("There's a path to " + lockedExits + " but it's overgrown with plants and tree limbs. You need to move them out of your way.\n");
      }
      else if (_game.CurrentRoom.Name.Equals("Beach"))
      {
        Messages.Add("You can tell the ship is to the south..but how do you get there? You might need to use this row boat..");
      }
      else if (_game.CurrentRoom.Name.Equals("Dark Cave"))
      {
        Messages.Add("There may be a path but it's too dark!.\n");
      }
      else if (_game.CurrentRoom.Name.Equals("row boat"))
      {
        Messages.Add("How are you going to get to the ship? It's so far away and these waves are pushing your boat all over.");
      }
      else
      {
        Messages.Add("There's a path to the " + exits + ". \n");
      }

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

        if (found.Name == "Row Boat")
        {
          _game.CurrentRoom = _game.CurrentRoom.Exits["west"];
          var key = "wooden paddle";
          var paddle = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == key);
          if (paddle == null)
          {
            _game.CurrentRoom = _game.CurrentRoom.Exits["west"];
            Messages.Add(_game.CurrentRoom.Description);
            EndRoom end = _game.CurrentRoom as EndRoom;
            if (end != null)
            {
              Messages.Add(end.Narrative);
            }
          }
          else
          {
            Messages.Add(_game.CurrentRoom.Description);
          }
        }
      }
      // check if item is in room
      else
      { Messages.Add("You don't have that Item"); }
    }
  }
}