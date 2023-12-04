namespace BrainRing.Core.Game;

public class BlitzRound : RoundBase
{
    public BlitzRound()
    {
        Topic = new Topic();
    }

    public Topic Topic { get; set; }

    public override List<Topic> GetTopics() => new() {Topic};
}