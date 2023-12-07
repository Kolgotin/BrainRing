using System.Text.Json.Serialization;

namespace BrainRing.Core.Game;

[JsonDerivedType(typeof(TextQuestion), typeDiscriminator: "t")]
[JsonDerivedType(typeof(ImageQuestion), typeDiscriminator: "i")]
public abstract class QuestionBase
{
    protected QuestionBase()
    {
        Description = "";
        Cost = 1;
    }

    public string Description { get; set; }
    public int Cost { get; set; }
}