using System;
using island_escape.Interfaces;

namespace island_escape.Models
{
  class Game : IGame
  {
    public IPlayer CurrentPlayer { get; set; }
    public IRoom CurrentRoom { get; set; }
    public Game()
    {
      Room cliff = new Room("Cliffside", "You're on the edge of a cliff overlooking the ocean. Be careful where you move so you don't fall.");
      Room field = new Room("Empty Field", "This field is vast but you don't see any other paths except the one you came from.");
      Room cave = new Room("Dark Cave", "You've fallen down a hole into a dark cave! You can't see anything because it is so dark.");
      Room beach = new Room("Beach", "You made it to the beach. Off the shore, too far to swim, you see a ship!");
      EndRoom ocean = new EndRoom("Ocean", "You turned your row boat the wrong direction..", false, "You can't get your small boat turned the back and you start to drift further away....Looks like you will be lost at sea forever.");
      EndRoom ship = new EndRoom("Ship", "You're rowing toward the ship!", true, "You climb aboard the ship and set sail! You made it off the island alive!");
      Room rb = new Room("Row Boat", "This rowboat is pretty worn...Will it make it to the ship?");

      Item machete = new Item("Machete", "You've stumbled upon a rusty but sturdy machete. I bet this will come in handy in the future.");
      Item paddle = new Item("Wooden Paddle", "You found a wooden paddle!");
      Item rowboat = new Item("Row Boat", "");
      Item torch = new Item("Torch and Matches", "");

      cliff.Exits.Add("west", field);
      field.Exits.Add("east", cliff);
      rb.Exits.Add("west", ocean);

      beach.AddLockedRoom(rowboat, "west", rb);
      rb.AddLockedRoom(paddle, "south", ship);
      cliff.AddLockedRoom(machete, "east", cave);
      cave.AddLockedRoom(torch, "north", beach);

      field.Items.Add(machete);
      field.Items.Add(torch);
      cave.Items.Add(paddle);
      beach.Items.Add(rowboat);

      CurrentRoom = cliff;

    }
  }
}