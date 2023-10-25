namespace BrainRing.Core.Game;

public class Topic
{
    public Topic()
    {
        Name = "";
        Questions = new List<Question>();
    }

    public string Name { get; set; }
    public List<Question> Questions { get; set; }
}