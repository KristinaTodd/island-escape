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

    }
  }
}