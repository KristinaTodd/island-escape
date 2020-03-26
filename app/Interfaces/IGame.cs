namespace island_escape.Interfaces
{
  interface IGame
  {
    IPlayer CurrentPlayer { get; set; }
    IRoom CurrentRoom { get; set; }
  }
}