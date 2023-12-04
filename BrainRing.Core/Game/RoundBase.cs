using System.Text.Json.Serialization;

namespace BrainRing.Core.Game;

[JsonDerivedType(typeof(SimpleRound), typeDiscriminator: "s")]
[JsonDerivedType(typeof(BlitzRound), typeDiscriminator: "b")]
public abstract class RoundBase
{
    protected RoundBase()
    {
        Name = "";
    }

    public string Name { get; set; }
    public abstract List<Topic> GetTopics();
}