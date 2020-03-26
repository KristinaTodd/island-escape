using System;
using island_escape.Controllers;
using island_escape.Interfaces;

namespace island_escape
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Clear();
      IGameController gc = new GameController();
      gc.Run();
    }
  }
}
