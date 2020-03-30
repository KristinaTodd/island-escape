using System;
using System.Threading;
using island_escape.Interfaces;
using island_escape.Services;

namespace island_escape.Controllers
{
  class GameController : IGameController
  {
    private IGameService _gs { get; set; }
    private bool _running { get; set; } = true;
    public void Run()
    {
      Console.WriteLine("Hello, what is your name?");
      _gs = new GameService(Console.ReadLine());
      Console.Clear();
      Console.WriteLine(@"
  __  ____  __     __   __ _  ____    ____  ____   ___   __   ____  ____ 
 (  )/ ___)(  )   / _\ (  ( \(    \  (  __)/ ___) / __) / _\ (  _ \(  __)
  )( \___ \/ (_/\/    \/    / ) D (   ) _) \___ \( (__ /    \ ) __/ ) _) 
 (__)(____/\____/\_/\_/\_)__)(____/  (____)(____/ \___)\_/\_/(__)  (____)

      ");
      Console.WriteLine("Hello...Welcome to the Island...");
      Console.WriteLine("Are you resourceful enough to escape? (y/n)");
      if (Console.ReadLine().ToLower() == "n")
      {
        _running = false;
      }
      string Warning = "Okay...You chose to continue at your own risk...";
      foreach (char letter in Warning)
      {
        Console.Write(letter);
        Thread.Sleep(70);
      }
      Thread.Sleep(500);
      while (_running)
      {
        GetUserInput();
        Print();
      }
    }
    public void GetUserInput()
    {
      Console.WriteLine("\nWhat would you like to do?\n");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();

      Console.Clear();
      switch (command)
      {
        case "quit":
          _running = false;
          break;
        case "reset":
          _gs.Reset();
          break;
        case "look":
          _gs.Messages.Clear();
          _gs.Look();
          break;
        case "inventory":
          _gs.Inventory();
          break;
        case "go":
          _gs.Messages.Clear();
          _running = _gs.Go(option);
          break;
        case "take":
          _gs.Take(option);
          break;
        case "use":
          _gs.Messages.Clear();
          _gs.Use(option);
          break;
        case "help":
          _gs.Help();
          break;
        default:
          _gs.Messages.Add("Not a recognized command");
          _gs.Look();
          break;
      }

    }
    public void Print()
    {
      foreach (string message in _gs.Messages)
      {
        Console.WriteLine(message);
      }
    }
  }

}