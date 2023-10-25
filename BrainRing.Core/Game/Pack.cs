namespace BrainRing.Core.Game;

public class Pack
{
    public Pack()
    {
        Name = "";
        Rounds = new List<Round>();
    }

    public string Name { get; set; }
    public List<Round> Rounds { get; set; }
}