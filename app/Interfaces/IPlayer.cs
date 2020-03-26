using System.Collections.Generic;

namespace island_escape.Interfaces
{
  interface IPlayer
  {
    string Name { get; set; }
    List<IItem> Inventory { get; set; }
  }
}