using System.Text.Json.Serialization;

namespace BrainRing.Core.Game;

[JsonDerivedType(typeof(TextQuestion), typeDiscriminator: "text")]
[JsonDerivedType(typeof(ImageQuestion), typeDiscriminator: "withImage")]
public class QuestionBase
{
    public QuestionBase()
    {
        Description = "";
        Cost = 1;
    }

    public string Description { get; set; }
    public int Cost { get; set; }
}