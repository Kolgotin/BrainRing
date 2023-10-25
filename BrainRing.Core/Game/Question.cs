namespace BrainRing.Core.Game;

public class Question
{
    public Question()
    {
        Description = "";
        Cost = 1;
    }

    public string Description { get; set; }
    public int Cost { get; set; }
}