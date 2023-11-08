namespace BrainRing.Core.Game;

public class ImageQuestion : QuestionBase
{
    public ImageQuestion()
    {
        ImageBase64 = "";
    }

    public string ImageBase64 { get; set; }
}