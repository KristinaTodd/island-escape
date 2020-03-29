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
      Room cliff = new Room("Ciffside", "You're on the edge of a cliff overlooking the ocean. Be careful where you move so you don't fall.");
      Room field = new Room("Empty Field", "There seems to be nothing here...");
      Room cave = new Room("Dark Cave", "You've fallen down a hole into a dark cave! You can't see anything because it is so dark.");
      Room beach = new Room("Beach", "You made it to the beach. Of the shore, to far to swim, you see a ship!");
      EndRoom ship = new EndRoom("Ship", "You're rowing toward the ship!", true, "You climb aboard the ship and set sail!");

      Item machete = new Item("Machete", "You've stumbled upon a rusty but sturdy machete. I bet this will come in handy in the future.");
      Item paddle = new Item("Wooden Paddle", "You found a wooden paddle!");
      Item rb = new Item("Row Boat", "This rowboat is pretty worn...Will it make it to the ship?");
      Item torch = new Item("Torch and Matches", "");

      cliff.Exits.Add("west", field);
      field.Exits.Add("east", cliff);
      cave.Exits.Add("north", beach);

      beach.AddLockedRoom(rb, "south", ship);
      cliff.AddLockedRoom(machete, "east", cave);

      field.Items.Add(machete);
      field.Items.Add(torch);
      cave.Items.Add(paddle);
      beach.Items.Add(rb);

      CurrentRoom = cliff;

    }
  }
}