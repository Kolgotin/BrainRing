namespace BrainRing.Core.Game;

public class Topic
{
    public Topic()
    {
        Name = "";
        Questions = new List<QuestionBase>();
    }

    public string Name { get; set; }
    public List<QuestionBase> Questions { get; set; }
}