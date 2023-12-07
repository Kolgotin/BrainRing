namespace BrainRing.Core.Game;

public class Pack
{
    public Pack()
    {
        Name = "";
        Rounds = new List<RoundBase>();
    }

    public string Name { get; set; }
    public List<RoundBase> Rounds { get; set; }
}