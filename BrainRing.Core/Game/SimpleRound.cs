namespace BrainRing.Core.Game;

public class SimpleRound : RoundBase
{
    public SimpleRound()
    {
        Topics = new List<Topic>();
    }

    public List<Topic> Topics { get; set; }

    public override List<Topic> GetTopics() => Topics;
}