namespace BrainRing.Core.Game;

public class Round
{
    public Round()
    {
        Name = "";
        Topics = new List<Topic>();
    }

    public string Name { get; set; }
    public List<Topic> Topics { get; set; }
}