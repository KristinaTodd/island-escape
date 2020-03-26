using System.Collections.Generic;

namespace island_escape.Interfaces
{
  interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<IItem> Items { get; set; }

    Dictionary<string, IRoom> Exits { get; set; }
    Dictionary<IItem, KeyValuePair<string, IRoom>> LockedExits { get; set; }

    string Use(IItem item);
  }
}