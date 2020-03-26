using island_escape.Interfaces;

namespace island_escape.Models
{
  class Item : IItem
  {
    public Item(string name, string description)
    {
      Name = name;
      Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
  }
}