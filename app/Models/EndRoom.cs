namespace island_escape.Models
{
  class EndRoom : Room
  {
    public bool Win { get; set; }
    public string Narrative { get; set; }
    public EndRoom(string name, string description, bool win, string narrative) : base(name, description)
    {
      Win = win;
      Narrative = narrative;
    }
  }
}